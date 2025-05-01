using Microsoft.Extensions.Logging;
using Moq;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Application.Handlers;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.Tests.Handlers
{
    public class EnqueueTollUsageCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Send_Message_To_Queue()
        {
            // Arrange
            var mockSender = new Mock<IMessageSender>();
            var mockLogger = new Mock<ILogger<EnqueueTollUsageCommandHandler>>();

            var handler = new EnqueueTollUsageCommandHandler(mockSender.Object, mockLogger.Object);

            var command = new EnqueueTollUsageCommand
            {
                Timestamp = DateTime.UtcNow,
                TollStation = "SP-101",
                City = "Campnas",
                State = "SP",
                AmountPaid = 12.5m,
                VehicleType = "Carro"
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockSender.Verify(s => s.SendLocal(It.IsAny<object>()), Times.Once);
        }
    }
}