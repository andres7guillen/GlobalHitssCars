using CarServiceApplication.Queries;
using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;
using CarServiceDomain.Repositories;
using Common.Logging.Interfaces;
using CSharpFunctionalExtensions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Tests.Queries
{
    public class GetCarByIdQueryTests
    {
        [Fact]
        public async void GetCarByIdShouldWorks_WhenACarIsFoundById() 
        {
            //Arrange
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

            mockCarRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(Car1);

            var handler = new GetCarByIdQuery.GetCarByIdQueryHandler(mockCarRepository.Object);
            var query = new GetCarByIdQuery(newGuid);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(Car1, result.Value);
            mockCarRepository.Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Once);

        }

        [Fact]
        public async void GetCarByIdShouldFails_WhenACarIsNotFoundById()
        {
            //Arrange
            var mockCarRepository = new Mock<ICarRepository>();
            var mockLogger = new Mock<ILogger>();
            var newGuid = Guid.NewGuid();

            mockCarRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(Maybe<Car>.None);

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
