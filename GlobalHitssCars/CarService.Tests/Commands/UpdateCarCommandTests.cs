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
            var car1 = Car.Build("Brand test1",
                2022,
                "Reference test",
                "Colour test1",
                "ABC123");
            var mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(repo => repo.Update(It.IsAny<Car>()))
                .ReturnsAsync(true);
            var command = new UpdateCarCommand(car1.Value);
            var handler = new UpdateCarCommand.UpdateCarCommandHandler(mockCarRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

        [Fact]
        public async void UpdateCarShouldFails()
        {
            //Arrange
            var car1 = Car.Build("Brand test1",
                2022,
                "Reference test",
                "Colour test1",
                "ABC123");
            var mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(repo => repo.Update(It.IsAny<Car>()))
                .ReturnsAsync(false);
            var command = new UpdateCarCommand(car1.Value);
            var handler = new UpdateCarCommand.UpdateCarCommandHandler(mockCarRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("4002: Error updating car.", result.Error);
            mockCarRepository.Verify(repo => repo.Update(It.IsAny<Car>()), Times.Once);
        }

    }
}
