using Thunders.TechTest.ApiService.Application.DTOs;
using Thunders.TechTest.ApiService.Application.Queries;
using Thunders.TechTest.ApiService.Interfaces;

namespace Thunders.TechTest.ApiService.Application.Handlers.QueryHandlers
{
    public class GetTopStationsByMonthHandler
    {
        private readonly ITollUsageRepository _repository;

        public GetTopStationsByMonthHandler(ITollUsageRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TopStationDto>> HandleAsync(GetTopStationsByMonthQuery query)
        {
            var from = new DateTime(query.Year, query.Month, 1);
            var to = from.AddMonths(1).AddTicks(-1);
            var usages = await _repository.GetByPeriodAsync(from, to);

            return usages
                .GroupBy(x => x.TollStation)
                .Select(g => new TopStationDto
                {
                    Station = g.Key,
                    Total = g.Sum(x => x.AmountPaid)
                })
                .OrderByDescending(x => x.Total)
                .Take(query.TopCount)
                .ToList();
        }
    }
}