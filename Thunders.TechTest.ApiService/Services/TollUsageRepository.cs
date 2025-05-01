using Microsoft.EntityFrameworkCore;
using System;
using Thunders.TechTest.ApiService.Interfaces;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Services;

namespace Thunders.TechTest.ApiService.Services
{
    public class TollUsageRepository : ITollUsageRepository
    {
        private readonly AppDbContext _context;

        public TollUsageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TollUsage usage)
        {
            _context.TollUsages.Add(usage);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TollUsage>> GetByPeriodAsync(DateTime from, DateTime to)
        {
            return await _context.TollUsages
                .Where(x => x.Timestamp >= from && x.Timestamp <= to)
                .ToListAsync();
        }

        public async Task<IEnumerable<TollUsage>> GetAllByStationAsync(string tollStation)
        {
            return await _context.TollUsages
                .Where(x => x.TollStation == tollStation)
                .ToListAsync();
        }
    }
}