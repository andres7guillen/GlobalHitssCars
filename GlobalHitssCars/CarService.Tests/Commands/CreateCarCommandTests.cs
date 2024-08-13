using CarServiceApplication.Commands;
using CarServiceApplication.Queries;
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
    public class CreateCarCommandTests
    {
        [Fact]
        public async void CreateCarShouldWorks() 
        {
            //arrange
            var mockCarRepository = new Mock<ICarRepository>();
            var mockLogger = new Mock<ILogger>();

            var newGuid = Guid.NewGuid();
            var Car1 = new Car()
            {
                Brand = "Brand test1",
                Colour = "Colour test1",
                Id = newGuid,
                LicensePlate = "Test1",
                Model = 1970,
                Reference = "Reference test1"
            };
            mockCarRepository.Setup(repo => repo.Create(It.IsAny<Car>()))
                .ReturnsAsync(Car1);

            var handler = new CreateCarCommand.CreateCarCommandHandler(mockCarRepository.Object, mockLogger.Object);
            var command = new CreateCarCommand(Car1);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(Car1, result.Value);
            mockCarRepository.Verify(repo => repo.Create(It.IsAny<Car>()), Times.Once);
        }

        [Fact]
        public async void CreateCarShouldFails()
        {
            //arrange
            var mockCarRepository = new Mock<ICarRepository>();
            var mockLogger = new Mock<ILogger>();

            var newGuid = Guid.NewGuid();
            var Car1 = new Car()
            {
                Brand = "Brand test1",
                Colour = "Colour test1",
                Id = newGuid,
                LicensePlate = "Test1",
                Model = 1970,
                Reference = "Reference test1"
            };
            mockCarRepository.Setup(repo => repo.Create(It.IsAny<Car>()))
                .ReturnsAsync((Car)null);

            var command = new CreateCarCommand(Car1);
            var handler = new CreateCarCommand.CreateCarCommandHandler(mockCarRepository.Object, mockLogger.Object);            

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("4003: Error creating car", result.Error);
            mockLogger.Verify(logger => logger.Error(It.IsAny<string>(), It.IsAny<Exception>()), Times.Once);
            mockCarRepository.Verify(repo => repo.Create(It.IsAny<Car>()), Times.Once);
        }

    }
}
