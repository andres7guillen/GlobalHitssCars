using Moq;
using PurchaseApplication.Commands;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Repository;

namespace PurchaseService.Tests.Commands
{
    public class UpdatePurchaseCommandTests
    {
        [Fact]
        public async void UpdatePurchaseShouldWorks()
        {
            //Arrange
            var purchaseExpected = Purchase.Build(
                Guid.NewGuid(),
                Guid.NewGuid(),
                70000000);
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            mockPurchaseRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(purchaseExpected.Value);
            mockPurchaseRepository.Setup(repo => repo.Update(It.IsAny<Purchase>()))
                .ReturnsAsync(true);
            var command = new UpdatePurchaseCommand(purchaseExpected.Value.Amount);
            var handler = new UpdatePurchaseCommand.UpdatePurchaseCommandHandler(mockPurchaseRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

        [Fact]
        public async void UpdatePurchaseShouldFails()
        {
            //Arrange
            var purchaseExpected = Purchase.Build(
                Guid.NewGuid(),
                Guid.NewGuid(),
                70000000);
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            mockPurchaseRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(purchaseExpected.Value);
            mockPurchaseRepository.Setup(repo => repo.Update(It.IsAny<Purchase>()))
                .ReturnsAsync(false);
            var command = new UpdatePurchaseCommand(purchaseExpected.Value.Amount);
            var handler = new UpdatePurchaseCommand.UpdatePurchaseCommandHandler(mockPurchaseRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("4002: Error updating Purchase", result.Error);
            mockPurchaseRepository.Verify(repo => repo.Update(It.IsAny<Purchase>()), Times.Once);
        }

    }
}
