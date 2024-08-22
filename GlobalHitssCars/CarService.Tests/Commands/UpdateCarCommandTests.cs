using CarServiceApplication.Commands;
using CarServiceDomain.Entities;
using CarServiceDomain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Tests.Commands
{
    public class UpdateCarCommandTests
    {
        [Fact]
        public async void UpdateCarShouldWorks() 
        {
            //Arrange
            var car1 = CarStock.Build(Guid.NewGuid(),
                2022,
                Guid.NewGuid(),
                "Colour test1");
            var mockCarRepository = new Mock<ICarStockRepository>();
            mockCarRepository.Setup(repo => repo.Update(It.IsAny<CarStock>()))
                .ReturnsAsync(true);
            mockCarRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(car1.Value);
            var command = new UpdateCarStockCommand(
                car1.Value.Id,
                car1.Value.Colour);
            var handler = new UpdateCarStockCommand.UpdateCarCommandHandler(mockCarRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            mockCarRepository.Verify(repo => repo.Update(It.IsAny<CarStock>()), Times.Once);

        }

        [Fact]
        public async void UpdateCarShouldFails()
        {
            //Arrange
            var car1 = CarStock.Build(Guid.NewGuid(),
                2022,
                Guid.NewGuid(),
                "Colour test1");
            var mockCarRepository = new Mock<ICarStockRepository>();
            mockCarRepository.Setup(repo => repo.Update(It.IsAny<CarStock>()))
                .ReturnsAsync(false);
            mockCarRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(car1.Value);
            var command = new UpdateCarStockCommand(
                car1.Value.Id,
                car1.Value.Colour);
            var handler = new UpdateCarStockCommand.UpdateCarCommandHandler(mockCarRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("4002: Error updating carStock.", result.Error);
            mockCarRepository.Verify(repo => repo.Update(It.IsAny<CarStock>()), Times.Once);
        }

    }
}
