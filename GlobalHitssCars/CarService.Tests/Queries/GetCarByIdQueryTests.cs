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
            var car1 = Car.Build("Brand test1",
                2021,
                "Reference test1",
                "Colour test1",
                "ABC123"
                );

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
