using Moq;
using PurchaseApplication.Commands;
using PurchaseServiceDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseService.Tests.Commands
{
    public class DeletePurchaseDeletePurchaseCommandTestsCommandTests
    {
        [Fact]
        public async void DeletePurchaseShouldWorks()
        {
            //arrange
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var newGuid = Guid.NewGuid();

            mockPurchaseRepository.Setup(repo => repo.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            //Act
            var command = new DeletePurchaseCommand(newGuid);
            var handler = new DeletePurchaseCommand.DeletePurchaseCommandHandler(mockPurchaseRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            mockPurchaseRepository.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void DeletePurchaseShouldFails()
        {
            //arrange
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var newGuid = Guid.NewGuid();

            mockPurchaseRepository.Setup(repo => repo.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(false);
            //Act
            var command = new DeletePurchaseCommand(newGuid);
            var handler = new DeletePurchaseCommand.DeletePurchaseCommandHandler(mockPurchaseRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("4004: Error deleting Purchase", result.Error);
            mockPurchaseRepository.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Once);
        }
    }
}
