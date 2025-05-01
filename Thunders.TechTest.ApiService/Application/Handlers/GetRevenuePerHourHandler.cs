using Thunders.TechTest.ApiService.Application.DTOs;
using Thunders.TechTest.ApiService.Application.Queries;
using Thunders.TechTest.ApiService.Interfaces;

namespace Thunders.TechTest.ApiService.Application.Handlers.QueryHandlers
{
    public class GetRevenuePerHourHandler
    {
        private readonly ITollUsageRepository _repository;

        public GetRevenuePerHourHandler(ITollUsageRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CityHourlyRevenueDto>> HandleAsync(GetRevenuePerHourQuery query)
        {
            var usages = await _repository.GetByPeriodAsync(query.From, query.To);

            return usages
                .GroupBy(x => x.City)
                .Select(cityGroup => new CityHourlyRevenueDto
                {
                    City = cityGroup.Key,
                    HourlyRevenues = cityGroup
                        .GroupBy(x => x.Timestamp.Hour)
                        .Select(hourGroup => new HourRevenueDto
                        {
                            Hour = hourGroup.Key,
                            Revenue = hourGroup.Sum(x => x.AmountPaid)
                        }).ToList()
                }).ToList();
        }
    }
}