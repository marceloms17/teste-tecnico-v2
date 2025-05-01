using Moq;
using Thunders.TechTest.ApiService.Application.Handlers.QueryHandlers;
using Thunders.TechTest.ApiService.Application.Queries;
using Thunders.TechTest.ApiService.Interfaces;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.ApiService.Models.Enums;

namespace Thunders.TechTest.Tests.Handlers
{
    public class GetVehicleCountByStationHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldReturnVehicleCount()
        {
            // Arrange
            var mockRepo = new Mock<ITollUsageRepository>();
            var handler = new GetVehicleCountByStationHandler(mockRepo.Object);

            var tollUsages = new List<TollUsage>
            {
                new TollUsage { TollStation = "SP-101", VehicleType = VehicleType.Carro },
                new TollUsage { TollStation = "SP-101", VehicleType = VehicleType.Carro },
                new TollUsage { TollStation = "SP-101", VehicleType = VehicleType.Caminhao }
            };

            mockRepo
                .Setup(r => r.GetAllByStationAsync("SP-101"))
                .ReturnsAsync(tollUsages);

            var query = new GetVehicleCountByStationQuery
            {
                TollStation = "SP-101"
            };

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            var car = result.FirstOrDefault(x => x.VehicleType == "Carro");
            var truck = result.FirstOrDefault(x => x.VehicleType == "Caminhao");

            Assert.Equal(2, car?.Count);
            Assert.Equal(1, truck?.Count);
        }
    }
}