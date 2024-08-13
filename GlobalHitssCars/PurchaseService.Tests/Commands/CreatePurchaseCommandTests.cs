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

            var newGuid = Guid.NewGuid();
            Purchase purchase1 = new Purchase()
            {
                Amount = 10,
                CarId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                Id = Guid.NewGuid()
            };
            mockPurchaseRepository.Setup(repo => repo.Create(It.IsAny<Purchase>()))
                .ReturnsAsync(purchase1);

            var command = new CreatePurchaseCommand(purchase1);
            var handler = new CreatePurchaseCommand.CreatePurchaseCommandHandler(mockPurchaseRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(purchase1, result.Value);
            mockPurchaseRepository.Verify(repo => repo.Create(It.IsAny<Purchase>()), Times.Once);
        }

        [Fact]
        public async void CreatePurchaseShouldFails()
        {
            //arrange
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockLogger = new Mock<ILogger>();

            var newGuid = Guid.NewGuid();
            Purchase purchase1 = new Purchase()
            {
                Amount = 10,
                CarId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                Id = Guid.NewGuid()
            };
            mockPurchaseRepository.Setup(repo => repo.Create(It.IsAny<Purchase>()))
                .ReturnsAsync((Purchase)null);

            var command = new CreatePurchaseCommand(purchase1);
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
