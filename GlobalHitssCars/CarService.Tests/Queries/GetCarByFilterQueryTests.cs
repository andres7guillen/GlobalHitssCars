using CarServiceApplication.Queries;
using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;
using CarServiceDomain.Exceptions;
using CarServiceDomain.Repositories;
using Common.Logging.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Tests.Queries
{
    public class GetCarByFilterQueryTests
    {
        [Fact]
        public async void GetCarByFilterShouldWorks_WhenCarsAreFound() 
        {
            //Arrange
            var mockCarRepository = new Mock<ICarStockRepository>();
            var mockLogger = new Mock<ILogger>();
            List<CarStock> cars = new List<CarStock>();
            var car1 = CarStock.Build(Guid.NewGuid(),
                2022,
                Guid.NewGuid(),
                "Colour test1");
            cars.Add(car1.Value);

            mockCarRepository.Setup(repo => repo.GetCarByFilter(It.IsAny<CarStockByFilterDTO>()))
                .ReturnsAsync(cars);

            var filter = new CarStockByFilterDTO()
            {
                BrandId = Guid.NewGuid(),
                Colour = "Colour test1"
            };

            var handler = new GetCarByFilterQuery.GetCarByFilterQueryHandler(mockCarRepository.Object);
            var query = new GetCarByFilterQuery(filter);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(cars, result.Value);
            mockCarRepository.Verify(repo => repo.GetCarByFilter(It.IsAny<CarStockByFilterDTO>()), Times.Once);

        }

        [Fact]
        public async void GetCarByFilterShouldFails_WhenNoCarsAreFound()
        {
            //Arrange
            var mockCarRepository = new Mock<ICarStockRepository>();
            var mockLogger = new Mock<ILogger>();
            List<CarStock> cars = new List<CarStock>();            

            mockCarRepository.Setup(repo => repo.GetCarByFilter(It.IsAny<CarStockByFilterDTO>()))
                .ReturnsAsync(cars);

            var filter = new CarStockByFilterDTO()
            {
                BrandId = Guid.NewGuid(),
                Colour = "Colour test1"
            };

            var handler = new GetCarByFilterQuery.GetCarByFilterQueryHandler(mockCarRepository.Object);
            var query = new GetCarByFilterQuery(filter);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(CarContextExceptionEnum.CarNotFoundByFilter.GetErrorMessage(), result.Error);
            mockCarRepository.Verify(repo => repo.GetCarByFilter(It.IsAny<CarStockByFilterDTO>()), Times.Once);
        }

    }
}
