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
            var mockCarRepository = new Mock<ICarStockRepository>();
            var mockLogger = new Mock<ILogger>();
            var expectedCar = CarStock.Build(
            withBrandId: Guid.NewGuid(),
            withModel: 2023,
            withReferenceId: Guid.NewGuid(),
            withColour: "Colour test1");


            mockCarRepository.Setup(repo => repo.Create(It.IsAny<CarStock>()))
            .ReturnsAsync(expectedCar.Value);

            var command = new CreateCarStockCommand(expectedCar.Value.BrandId, 
                expectedCar.Value.Model, 
                expectedCar.Value.ReferenceId, 
                expectedCar.Value.Colour);
            var handler = new CreateCarStockCommand.CreateCarCommandHandler(mockCarRepository.Object, mockLogger.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);

            mockCarRepository.Verify(repo => repo.Create(It.IsAny<CarStock>()), Times.Once);
        }

        [Fact]
        public async void CreateCarShouldFails_WhenModelIsIncorrect()
        {
            //arrange
            var expectedCar = CarStock.Build(
            withBrandId: Guid.NewGuid(),
            withModel: 2023,
            withReferenceId: Guid.NewGuid(),
            withColour: "Colour test1");
            var mockCarRepository = new Mock<ICarStockRepository>();
            var mockLogger = new Mock<ILogger>();

            mockCarRepository.Setup(repo => repo.Create(It.IsAny<CarStock>()))
                .ReturnsAsync((CarStock car) => null);
            var command = new CreateCarStockCommand(expectedCar.Value.BrandId,
                expectedCar.Value.Model,
                expectedCar.Value.ReferenceId,
                expectedCar.Value.Colour);
            var handler = new CreateCarStockCommand.CreateCarCommandHandler(mockCarRepository.Object, mockLogger.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            mockLogger.Verify(logger => logger.Error(It.IsAny<string>(), It.IsAny<Exception>()), Times.Once);
            mockCarRepository.Verify(repo => repo.Create(It.IsAny<CarStock>()), Times.Once);

        }

    }
}
