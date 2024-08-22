using CarServiceApplication.Queries;
using CarServiceDomain.Entities;
using CarServiceDomain.Repositories;
using Common.Logging.Interfaces;
using CSharpFunctionalExtensions;
using Moq;

namespace CarService.Tests.Queries
{
    public class GetCarByIdQueryTests
    {
        [Fact]
        public async void GetCarByIdShouldWorks_WhenACarIsFoundById()
        {
            //Arrange
            var mockCarRepository = new Mock<ICarStockRepository>();
            var mockLogger = new Mock<ILogger>();
            var car1 = CarStock.Build(Guid.NewGuid(),
                2022,
                Guid.NewGuid(),
                "Colour test1");

            mockCarRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(car1.Value);

            var handler = new GetCarByIdQuery.GetCarByIdQueryHandler(mockCarRepository.Object);
            var query = new GetCarByIdQuery(car1.Value.Id);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(car1.Value, result.Value);
            mockCarRepository.Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Once);

        }

        [Fact]
        public async void GetCarByIdShouldFails_WhenACarIsNotFoundById()
        {
            //Arrange
            var mockCarRepository = new Mock<ICarStockRepository>();
            var mockLogger = new Mock<ILogger>();
            var newGuid = Guid.NewGuid();

            mockCarRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(Maybe<CarStock>.None);

            var handler = new GetCarByIdQuery.GetCarByIdQueryHandler(mockCarRepository.Object);
            var query = new GetCarByIdQuery(newGuid);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            mockCarRepository.Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Once);
        }
    }
}
