using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.ApiService.Interfaces
{
    public interface ITollUsageRepository
    {
        Task AddAsync(TollUsage usage);

        Task<IEnumerable<TollUsage>> GetByPeriodAsync(DateTime from, DateTime to);

        Task<IEnumerable<TollUsage>> GetAllByStationAsync(string tollStation);
    }
}