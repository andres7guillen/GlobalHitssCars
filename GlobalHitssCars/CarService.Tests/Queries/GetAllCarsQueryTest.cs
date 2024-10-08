﻿using CarServiceApplication.Queries;
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
            var mockCarRepository = new Mock<ICarStockRepository>();
            var mockLogger = new Mock<ILogger>();
            var cars = new List<CarStock>();
            var car1 = CarStock.Build(Guid.NewGuid(),
                2022,
                Guid.NewGuid(),
                "Colour test1");
            var car2 = CarStock.Build(Guid.NewGuid(),
                2022,
                Guid.NewGuid(),
                "Colour test1");
            cars.Add(car1.Value);
            cars.Add(car2.Value);
            mockCarRepository.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Tuple<int, IEnumerable<CarStock>>(cars.Count(), cars));

            var handler = new GetAllCarsQuery.GetAllCarsQueryHandler(mockCarRepository.Object, mockLogger.Object);
            var query = new GetAllCarsQuery(offset: 0, limit: 10);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(cars, result.Value.Item2);
            mockCarRepository.Verify(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public async Task GetAllCarsQueryShouldFail_WhenNoCarsAreFound()
        {
            // Arrange
            var mockCarRepository = new Mock<ICarStockRepository>();
            var mockLogger = new Mock<ILogger>();
            var emptyCars = new List<CarStock>();

            mockCarRepository.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                             .ReturnsAsync(new Tuple<int, IEnumerable<CarStock>>(emptyCars.Count(), emptyCars));

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
