using Common.Logging.Interfaces;
using Moq;
using PurchaseApplication.Commands;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseService.Tests.Commands
{
    public class CreatePurchaseCommandTests
    {
        [Fact]
        public async void CreatePurchaseShouldWorks()
        {
            //arrange
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockLogger = new Mock<ILogger>();

            var purchaseExpected = Purchase.Build(
                Guid.NewGuid(),
                Guid.NewGuid(),
                70000000);                
            
            mockPurchaseRepository.Setup(repo => repo.Create(It.IsAny<Purchase>()))
                .ReturnsAsync(purchaseExpected.Value);

            var command = new CreatePurchaseCommand(purchaseExpected.Value.ClientId, purchaseExpected.Value.CarId,purchaseExpected.Value.Amount);
            var handler = new CreatePurchaseCommand.CreatePurchaseCommandHandler(mockPurchaseRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(purchaseExpected.Value, result.Value);
            mockPurchaseRepository.Verify(repo => repo.Create(It.IsAny<Purchase>()), Times.Once);
        }

        [Fact]
        public async void CreatePurchaseShouldFails()
        {
            //arrange
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockLogger = new Mock<ILogger>();

            var purchaseExpected = Purchase.Build(
                Guid.NewGuid(),
                Guid.NewGuid(),
                70000000);
            mockPurchaseRepository.Setup(repo => repo.Create(It.IsAny<Purchase>()))
                .ReturnsAsync((Purchase)null);

            var command = new CreatePurchaseCommand(purchaseExpected.Value.ClientId,purchaseExpected.Value.CarId, purchaseExpected.Value.Amount);
            var handler = new CreatePurchaseCommand.CreatePurchaseCommandHandler(mockPurchaseRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("4003: Error creating Purchase", result.Error);
            mockPurchaseRepository.Verify(repo => repo.Create(It.IsAny<Purchase>()), Times.Once);
        }

    }
}
