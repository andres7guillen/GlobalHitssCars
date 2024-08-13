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
    public class DeleteSparePartCommandTests
    {
        [Fact]
        public async void DeleteSparePartShouldWorks()
        {
            //arrange
            var mockSparePartRepository = new Mock<ISparePartRepository>();
            var newGuid = Guid.NewGuid();

            mockSparePartRepository.Setup(repo => repo.DeleteSparePart(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            //Act
            var command = new DeleteSparePartCommand(newGuid);
            var handler = new DeleteSparePartCommand.DeleteSparePartCommandHandler(mockSparePartRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            mockSparePartRepository.Verify(repo => repo.DeleteSparePart(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void DeleteSparePartShouldFails()
        {
            //arrange
            var mockSparePartRepository = new Mock<ISparePartRepository>();
            var newGuid = Guid.NewGuid();

            mockSparePartRepository.Setup(repo => repo.DeleteSparePart(It.IsAny<Guid>()))
                .ReturnsAsync(false);
            //Act
            var command = new DeleteSparePartCommand(newGuid);
            var handler = new DeleteSparePartCommand.DeleteSparePartCommandHandler(mockSparePartRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("4004: Error deleting SparePart", result.Error);
            mockSparePartRepository.Verify(repo => repo.DeleteSparePart(It.IsAny<Guid>()), Times.Once);
        }
    }
}
