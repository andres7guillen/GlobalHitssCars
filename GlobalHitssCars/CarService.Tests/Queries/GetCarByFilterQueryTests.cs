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
            var mockCarRepository = new Mock<ICarRepository>();
            var mockLogger = new Mock<ILogger>();
            List<Car> cars = new List<Car>();
            var car1 = Car.Build("Brand test1",
                2021,
                "Reference test1",
                "Colour test1",
                "ABC123"
                );
            cars.Add(car1.Value);

            mockCarRepository.Setup(repo => repo.GetCarByFilter(It.IsAny<CarByFilterDTO>()))
                .ReturnsAsync(cars);

            var filter = new CarByFilterDTO()
            {
                Brand = "Brand test1",
                Colour = "Colour test1"
            };

            var handler = new GetCarByFilterQuery.GetCarByFilterQueryHandler(mockCarRepository.Object);
            var query = new GetCarByFilterQuery(filter);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(cars, result.Value);
            mockCarRepository.Verify(repo => repo.GetCarByFilter(It.IsAny<CarByFilterDTO>()), Times.Once);

        }

        [Fact]
        public async void GetCarByFilterShouldFails_WhenNoCarsAreFound()
        {
            //Arrange
            var mockCarRepository = new Mock<ICarRepository>();
            var mockLogger = new Mock<ILogger>();
            List<Car> cars = new List<Car>();            

            mockCarRepository.Setup(repo => repo.GetCarByFilter(It.IsAny<CarByFilterDTO>()))
                .ReturnsAsync(cars);

            var filter = new CarByFilterDTO()
            {
                Brand = "Brand test1",
                Colour = "Colour test1"
            };

            var handler = new GetCarByFilterQuery.GetCarByFilterQueryHandler(mockCarRepository.Object);
            var query = new GetCarByFilterQuery(filter);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(CarContextExceptionEnum.CarNotFoundByFilter.GetErrorMessage(), result.Error);
            mockCarRepository.Verify(repo => repo.GetCarByFilter(It.IsAny<CarByFilterDTO>()), Times.Once);
        }

    }
}
