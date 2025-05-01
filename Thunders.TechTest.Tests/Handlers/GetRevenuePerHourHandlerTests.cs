using Moq;
using Thunders.TechTest.ApiService.Application.Handlers.QueryHandlers;
using Thunders.TechTest.ApiService.Application.Queries;
using Thunders.TechTest.ApiService.Interfaces;
using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.Tests.Handlers
{
    public class GetRevenuePerHourHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldReturnGroupedRevenuePerHour()
        {
            // Arrange
            var mockRepo = new Mock<ITollUsageRepository>();
            var handler = new GetRevenuePerHourHandler(mockRepo.Object);

            var usages = new List<TollUsage>
            {
                new TollUsage { City = "Campinas", Timestamp = new DateTime(2025, 4, 30, 14, 0, 0), AmountPaid = 10 },
                new TollUsage { City = "Campinas", Timestamp = new DateTime(2025, 4, 30, 14, 30, 0), AmountPaid = 5 },
                new TollUsage { City = "Sao Paulo", Timestamp = new DateTime(2025, 4, 30, 15, 0, 0), AmountPaid = 30 },
            };

            mockRepo
                .Setup(r => r.GetByPeriodAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(usages);

            var query = new GetRevenuePerHourQuery
            {
                From = new DateTime(2025, 4, 30, 0, 0, 0),
                To = new DateTime(2025, 4, 30, 23, 59, 59)
            };

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            var campinas = result.First(r => r.City == "Campinas");
            Assert.Single(campinas.HourlyRevenues);
            Assert.Equal(15, campinas.HourlyRevenues.First().Revenue);

            var sp = result.First(r => r.City == "São Paulo");
            Assert.Single(sp.HourlyRevenues);
            Assert.Equal(30, sp.HourlyRevenues.First().Revenue);
        }
    }
}