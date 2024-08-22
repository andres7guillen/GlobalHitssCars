using Moq;
using SparePartServiceApplication.Commands;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Events;
using SparePartsServiceDomain.Exceptions;
using SparePartsServiceDomain.Repositories;
using SparePartsServiceDomain.SharedKernel;

namespace SparePartsService.Tests.Commands
{
    public class LessStockSparePartCommandHandlerTests
    {
        [Fact]
        public async Task ShouldReturnFailure_WhenThereIsNotEnoughStock()
        {
            // Arrange FAIIIL
            var sparePartRepositoryMock = new Mock<ISparePartRepository>();
            var sparePart = SparePart.Build("SampleSparePart", "BrandSpareTest", "BrandCarTest", 2000, "referenceTest", true, 10).Value;
            var command = new LessStockSparePartCommand(Guid.NewGuid(), 50);
            var handler = new LessStockSparePartCommand.LessStockSparePartCommandHandler(sparePartRepositoryMock.Object);

            sparePartRepositoryMock
                .Setup(repo => repo.GetSparePartById(It.IsAny<Guid>()))
                .ReturnsAsync(sparePart);
            sparePartRepositoryMock
                .Setup(repo => repo.UpdatateSpare(It.IsAny<SparePart>()))
                .ReturnsAsync(true);
            //messageProducerMock
            //    .Setup(m => m.SendingMessage(It.IsAny<Event<object>>(), It.IsAny<string>()))
            //    .Verifiable();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(SparePartContextExceptionEnum.IsNotEnoughStockToDelete.GetErrorMessage(), result.Error);
        }

        [Fact]
        public async Task ShouldReturnSuccess_WhenThereIsEnoughStock()
        {
            // Arrange
            var sparePartRepositoryMock = new Mock<ISparePartRepository>();
            //var messageProducerMock = new Mock<IMessageProducer>();
            var sparePart = SparePart.Build("SampleSparePart", "BrandSpareTest", "BrandCarTest", 2000, "referenceTest", true, 10).Value;
            var command = new LessStockSparePartCommand(Guid.NewGuid(), 5);
            var handler = new LessStockSparePartCommand.LessStockSparePartCommandHandler(sparePartRepositoryMock.Object/*, messageProducerMock.Object*/);

            sparePartRepositoryMock
                .Setup(repo => repo.GetSparePartById(It.IsAny<Guid>()))
                .ReturnsAsync(sparePart);
            sparePartRepositoryMock
                .Setup(repo => repo.UpdatateSpare(It.IsAny<SparePart>()))
                .ReturnsAsync(true);
            //messageProducerMock
            //    .Setup(m => m.SendingMessage(It.IsAny<Event<object>>(), It.IsAny<string>()))
            //    .Verifiable();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            sparePartRepositoryMock.Verify(repo => repo.GetSparePartById(It.IsAny<Guid>()), Times.Once);
            //messageProducerMock.Verify(m => m.SendingMessage(It.IsAny<Event<LessStockSparePartEvent>>(), "LessStockQueue"), Times.Once);
        }
    }
}
