using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Thunders.TechTest.ApiService.Application.DTOs;
using Thunders.TechTest.ApiService.Application.Handlers.QueryHandlers;
using Thunders.TechTest.ApiService.Application.Queries;
using Thunders.TechTest.ApiService.Interfaces;
using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.Tests.Handlers
{
    public class GetTopStationsByMonthHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldReturnTopStations()
        {
            // Arrange
            var mockRepo = new Mock<ITollUsageRepository>();

            var tollUsages = new List<TollUsage>
            {
                new TollUsage { TollStation = "SP-101", Timestamp = new DateTime(2025, 4, 1), AmountPaid = 10 },
                new TollUsage { TollStation = "SP-101", Timestamp = new DateTime(2025, 4, 1), AmountPaid = 15 },
                new TollUsage { TollStation = "RJ-222", Timestamp = new DateTime(2025, 4, 1), AmountPaid = 5 },
            };

            mockRepo
                .Setup(r => r.GetByPeriodAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(tollUsages);

            var handler = new GetTopStationsByMonthHandler(mockRepo.Object);

            var query = new GetTopStationsByMonthQuery
            {
                Year = 2025,
                Month = 4,
                TopCount = 1
            };

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("SP-101", result.First().Station);
            Assert.Equal(25, result.First().Total);
        }
    }
}