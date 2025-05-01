using Thunders.TechTest.ApiService.Application.DTOs;
using Thunders.TechTest.ApiService.Application.Queries;
using Thunders.TechTest.ApiService.Interfaces;

namespace Thunders.TechTest.ApiService.Application.Handlers.QueryHandlers
{
    public class GetVehicleCountByStationHandler
    {
        private readonly ITollUsageRepository _repository;

        public GetVehicleCountByStationHandler(ITollUsageRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<VehicleCountDto>> HandleAsync(GetVehicleCountByStationQuery query)
        {
            var usages = await _repository.GetAllByStationAsync(query.TollStation);

            return usages
                .GroupBy(x => x.VehicleType)
                .Select(g => new VehicleCountDto
                {
                    VehicleType = g.Key.ToString(),
                    Count = g.Count()
                })
                .ToList();
        }
    }
}