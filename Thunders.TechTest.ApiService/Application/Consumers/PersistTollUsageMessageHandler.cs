using Rebus.Handlers;
using Thunders.TechTest.ApiService.Application.Messages;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Models.Enums;
using Thunders.TechTest.ApiService.Services;

namespace Thunders.TechTest.ApiService.Application.Consumers
{
    public class PersistTollUsageMessageHandler : IHandleMessages<RegisterTollUsageMessage>
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PersistTollUsageMessageHandler> _logger;

        public PersistTollUsageMessageHandler(AppDbContext context, ILogger<PersistTollUsageMessageHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(RegisterTollUsageMessage message)
        {
            _logger.LogInformation("Consumindo mensgem da fila: {@Message}", message);

            if (!Enum.TryParse<VehicleType>(message.VehicleType, ignoreCase: true, out var vehicleType))
            {
                _logger.LogWarning("Tipo de veículo invalido na menssagem da fila: {Tipo}", message.VehicleType);
                return;
            }

            var tollUsage = new TollUsage
            {
                Id = Guid.NewGuid(),
                Timestamp = message.Timestamp,
                TollStation = message.TollStation,
                City = message.City,
                State = message.State,
                AmountPaid = message.AmountPaid,
                VehicleType = vehicleType
            };

            await _context.TollUsages.AddAsync(tollUsage);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Mensagem processada com sucesso. Registro inserido com Id: {Id}", tollUsage.Id);
        }
    }
}