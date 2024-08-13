using Moq;
using SparePartServiceApplication.Commands;
using SparePartsServiceDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsService.Tests.Commands
{
    public class AddSpareStockCommandTests
    {
        [Fact]
        public async void AddSpareStockShouldWorks()
        {
            //arrange
            var mockSparePartRepository = new Mock<ISparePartRepository>();
            var newGuid = Guid.NewGuid();
            var quantity = 10;

            mockSparePartRepository.Setup(repo => repo.AddStock(It.IsAny<Guid>(), It.IsAny<int>()))
                .ReturnsAsync(true);
            //Act
            var command = new AddSpareStockCommand(newGuid, quantity);
            var handler = new AddSpareStockCommand.AddSpareStockCommandHandler(mockSparePartRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            mockSparePartRepository.Verify(repo => repo.AddStock(It.IsAny<Guid>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void AddSpareStockShouldFails()
        {
            //arrange
            var mockSparePartRepository = new Mock<ISparePartRepository>();
            var newGuid = Guid.NewGuid();
            var quantity = 10;

            mockSparePartRepository.Setup(repo => repo.AddStock(It.IsAny<Guid>(), It.IsAny<int>()))
                .ReturnsAsync(false);
            //Act
            var command = new AddSpareStockCommand(newGuid, quantity);
            var handler = new AddSpareStockCommand.AddSpareStockCommandHandler(mockSparePartRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("4002: Error updating SparePart", result.Error);
            mockSparePartRepository.Verify(repo => repo.AddStock(It.IsAny<Guid>(), It.IsAny<int>()), Times.Once);
        }
    }
}
