using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Models.Enums;
using Thunders.TechTest.ApiService.Services;

namespace Thunders.TechTest.Tests.Repositories
{
    public class TollUsageRepositoryTests
    {
        private AppDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldPersistTollUsage()
        {
            // Arrange
            var context = CreateInMemoryContext();
            var repository = new TollUsageRepository(context);

            var usage = new TollUsage
            {
                Timestamp = DateTime.UtcNow,
                TollStation = "SP-101",
                City = "Campinas",
                State = "SP",
                AmountPaid = 12.5m,
                VehicleType = VehicleType.Car
            };

            // Act
            await repository.AddAsync(usage);
            var result = await context.TollUsages.FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SP-101", result.TollStation);
            Assert.Equal(12.5m, result.AmountPaid);
        }

        [Fact]
        public async Task GetByPeriodAsync_ShouldReturnOnlyUsagesWithinRange()
        {
            // Arrange
            var context = CreateInMemoryContext();
            var repository = new TollUsageRepository(context);

            await context.TollUsages.AddRangeAsync(new List<TollUsage>
            {
                new TollUsage { Timestamp = new DateTime(2025, 4, 1), TollStation = "A", VehicleType = VehicleType.Carro, City = "Campinas", State = "SP"},
                new TollUsage { Timestamp = new DateTime(2025, 4, 5), TollStation = "B", VehicleType = VehicleType.Caminhao, City = "Maringa", State = "PR"},
                new TollUsage { Timestamp = new DateTime(2025, 4, 10), TollStation = "C", VehicleType = VehicleType.Moto, City = "Osasco", State = "SP"}
            });
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetByPeriodAsync(new DateTime(2025, 4, 2), new DateTime(2025, 4, 9));

            // Assert
            Assert.Single(result);
            Assert.Equal("B", ((TollUsage)result.First()).TollStation);
        }

        [Fact]
        public async Task GetAllByStationAsync_ShouldReturnMatchingStationRecords()
        {
            // Arrange
            var context = CreateInMemoryContext();
            var repository = new TollUsageRepository(context);

            await context.TollUsages.AddRangeAsync(new List<TollUsage>
            {
                new TollUsage { TollStation = "SP-101", VehicleType = VehicleType.Moto, City = "Campinas", State = "SP" },
                new TollUsage { TollStation = "SP-101", VehicleType = VehicleType.Carro, City = "Maringa", State = "PR" },
                new TollUsage { TollStation = "RJ-222", VehicleType = VehicleType.Caminhao, City = "Cabo frio", State = "RJ" }
            });
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetAllByStationAsync("SP-101");

            // Assert
            Assert.Equal(2, result.Count());
        }
    }
}