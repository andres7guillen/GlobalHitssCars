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
    public class UpdatePurchaseCommandTests
    {
        [Fact]
        public async void UpdatePurchaseShouldWorks()
        {
            //Arrange
            var newGuid = Guid.NewGuid();
            Purchase purchase1 = new Purchase()
            {
                Amount = 10,
                CarId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                Id = Guid.NewGuid()
            };
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            mockPurchaseRepository.Setup(repo => repo.Update(It.IsAny<Purchase>()))
                .ReturnsAsync(true);
            var command = new UpdatePurchaseCommand(purchase1);
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
            var newGuid = Guid.NewGuid();
            Purchase purchase1 = new Purchase()
            {
                Amount = 10,
                CarId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                Id = Guid.NewGuid()
            };
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            mockPurchaseRepository.Setup(repo => repo.Update(It.IsAny<Purchase>()))
                .ReturnsAsync(false);
            var command = new UpdatePurchaseCommand(purchase1);
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
