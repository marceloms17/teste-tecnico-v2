using MediatR;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Application.Messages;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.ApiService.Application.Handlers
{
    public class EnqueueTollUsageCommandHandler : IRequestHandler<EnqueueTollUsageCommand, Unit>
    {
        private readonly IMessageSender _messageSender;
        private readonly ILogger<EnqueueTollUsageCommandHandler> _logger;

        public EnqueueTollUsageCommandHandler(IMessageSender messageSender, ILogger<EnqueueTollUsageCommandHandler> logger)
        {
            _messageSender = messageSender;
            _logger = logger;
        }

        public async Task<Unit> Handle(EnqueueTollUsageCommand request, CancellationToken cancellationToken)
        {
            var message = new RegisterTollUsageMessage
            {
                Timestamp = request.Timestamp,
                TollStation = request.TollStation,
                City = request.City,
                State = request.State,
                AmountPaid = request.AmountPaid,
                VehicleType = request.VehicleType
            };

            await _messageSender.SendLocal(message);

            _logger.LogInformation("Mensagem enviada pra a fila: {@Message}", message);

            return Unit.Value;
        }
    }
}