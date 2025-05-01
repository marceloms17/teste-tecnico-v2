using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Thunders.TechTest.ApiService.Application.DTOs;
using Thunders.TechTest.ApiService.Application.Queries;
using Thunders.TechTest.ApiService.Controllers;
using Thunders.TechTest.ApiService.Interfaces;

namespace Thunders.TechTest.Tests.Controllers
{
    public class TollUsageControllerTests
    {
        [Fact]
        public async Task GetRevenueByHour_ShouldReturnOkWithData()
        {
            // Arrange
            var mockRepo = new Mock<ITollUsageRepository>();
            var mockMediator = new Mock<IMediator>();

            var sampleData = new List<CityHourlyRevenueDto>
    {
        new CityHourlyRevenueDto
        {
            City = "Campinas",
            HourlyRevenues = new List<HourRevenueDto>
            {
                new HourRevenueDto
                {
                    Hour = 10,
                    Revenue = 20.0m
                }
            }
        }
    };

            mockMediator
            .Setup(m => m.Send(It.IsAny<GetRevenuePerHourQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(sampleData);

            var controller = new TollUsageController(mockRepo.Object, mockMediator.Object);

            var from = new DateTime(2025, 4, 30);
            var to = new DateTime(2025, 4, 30);

            // Act
            var result = await controller.GetRevenueByHour(from, to);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<List<CityHourlyRevenueDto>>(okResult.Value);
            Assert.Single(value);
            Assert.Equal("Campinas", value[0].City);
            Assert.Equal(20.0m, value[0].HourlyRevenues[0].Revenue);
        }
    }
}