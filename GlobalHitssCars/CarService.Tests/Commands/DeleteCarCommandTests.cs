using CarServiceApplication.Commands;
using CarServiceDomain.Entities;
using CarServiceDomain.Repositories;
using Common.Logging.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Tests.Commands
{
    public class DeleteCarCommandTests
    {
        [Fact]
        public async void DeleteCarShouldWorks() 
        {
            //arrange
            var mockCarRepository = new Mock<ICarStockRepository>();
            var newGuid = Guid.NewGuid();
            
            mockCarRepository.Setup(repo => repo.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            //Act
            var command = new DeleteCarStockCommand(newGuid);
            var handler = new DeleteCarStockCommand.DeleteCarCommandHandler(mockCarRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            mockCarRepository.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void DeleteCarShouldFails()
        {
            //arrange
            var mockCarRepository = new Mock<ICarStockRepository>();
            var newGuid = Guid.NewGuid();

            mockCarRepository.Setup(repo => repo.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(false);
            //Act
            var command = new DeleteCarStockCommand(newGuid);
            var handler = new DeleteCarStockCommand.DeleteCarCommandHandler(mockCarRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("4004: Error deleting carStock.", result.Error);
            mockCarRepository.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Once);
        }

    }
}
