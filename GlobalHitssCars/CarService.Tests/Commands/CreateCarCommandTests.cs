using CarServiceApplication.Commands;
using CarServiceDomain.Entities;
using CarServiceDomain.Repositories;
using Common.Logging.Interfaces;
using CSharpFunctionalExtensions;
using Moq;

namespace CarService.Tests.Commands
{
    public class CreateCarCommandTests
    {
        [Fact]
        public async void CreateCarShouldWorks()
        {
            // Arrange
            var mockCarRepository = new Mock<ICarRepository>();
            var mockLogger = new Mock<ILogger>();
            var expectedCar = Car.Build(
            withBrand: "Brand test1",
            withModel: 2023,
            withReference: "Reference test1",
            withColour: "Colour test1",
            withLicensePlate: "ABC123");


            mockCarRepository.Setup(repo => repo.Create(It.IsAny<Car>()))
            .ReturnsAsync(expectedCar.Value);

            var command = new CreateCarCommand(expectedCar.Value.Brand, 
                expectedCar.Value.Model, 
                expectedCar.Value.Reference, 
                expectedCar.Value.Colour, 
                expectedCar.Value.LicensePlate);
            var handler = new CreateCarCommand.CreateCarCommandHandler(mockCarRepository.Object, mockLogger.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);

            mockCarRepository.Verify(repo => repo.Create(It.IsAny<Car>()), Times.Once);
        }

        [Fact]
        public async void CreateCarShouldFails_WhenModelIsIncorrect()
        {
            //arrange
            var expectedCar = Car.Build(
            withBrand: "Brand test1",
            withModel: 2023,
            withReference: "Reference test1",
            withColour: "Colour test1",
            withLicensePlate: "ABC123");
            var mockCarRepository = new Mock<ICarRepository>();
            var mockLogger = new Mock<ILogger>();

            mockCarRepository.Setup(repo => repo.Create(It.IsAny<Car>()))
                .ReturnsAsync((Car car) => null);
            var command = new CreateCarCommand(expectedCar.Value.Brand,
                expectedCar.Value.Model,
                expectedCar.Value.Reference,
                expectedCar.Value.Colour,
                expectedCar.Value.LicensePlate);
            var handler = new CreateCarCommand.CreateCarCommandHandler(mockCarRepository.Object, mockLogger.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            mockLogger.Verify(logger => logger.Error(It.IsAny<string>(), It.IsAny<Exception>()), Times.Once);
            mockCarRepository.Verify(repo => repo.Create(It.IsAny<Car>()), Times.Once);

        }

    }
}
