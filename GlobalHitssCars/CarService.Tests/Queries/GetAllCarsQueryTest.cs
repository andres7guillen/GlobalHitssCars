using CarServiceApplication.Queries;
using CarServiceDomain.Entities;
using CarServiceDomain.Exceptions;
using CarServiceDomain.Repositories;
using Common.Logging.Interfaces;
using Moq;

namespace CarService.Tests.Queries
{
    public class GetAllCarsQueryTest
    {       
        [Fact]
        public async void GetAllCarsQueryShouldWorks_WhenCarAreFound()
        {
            //Arrange
            var mockCarRepository = new Mock<ICarRepository>();
            var mockLogger = new Mock<ILogger>();
            var cars = new List<Car>();
            var Car1 = new Car()
            {
                Brand = "Brand test1",
                Colour = "Colour test1",
                Id = Guid.NewGuid(),
                LicensePlate = "Test1",
                Model = 1970,
                Reference = "Reference test1"
            };
            var Car2 = new Car()
            {
                Brand = "Brand test2",
                Colour = "Colour test2",
                Id = Guid.NewGuid(),
                LicensePlate = "Test2",
                Model = 1970,
                Reference = "Reference test2"
            };
            cars.Add(Car1);
            cars.Add(Car2);
            mockCarRepository.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cars);

            var handler = new GetAllCarsQuery.GetAllCarsQueryHandler(mockCarRepository.Object, mockLogger.Object);
            var query = new GetAllCarsQuery(offset: 0, limit: 10);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(cars, result.Value);
            mockCarRepository.Verify(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public async Task GetAllCarsQueryShouldFail_WhenNoCarsAreFound()
        {
            // Arrange
            var mockCarRepository = new Mock<ICarRepository>();
            var mockLogger = new Mock<ILogger>();
            var emptyCars = new List<Car>();

            mockCarRepository.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                             .ReturnsAsync(emptyCars);

            var handler = new GetAllCarsQuery.GetAllCarsQueryHandler(mockCarRepository.Object, mockLogger.Object);
            var query = new GetAllCarsQuery(offset: 0, limit: 10);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(CarContextExceptionEnum.NoCarsFound.GetErrorMessage(), result.Error);
            mockCarRepository.Verify(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            mockLogger.Verify(logger => logger.Error(It.IsAny<string>(), It.IsAny<Exception>()), Times.Once);
        }
    }
}
