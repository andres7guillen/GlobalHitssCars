﻿using Common.Logging.Interfaces;
using CSharpFunctionalExtensions;
using Moq;
using PurchaseApplication.Queries;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Repository;

namespace PurchaseService.Tests.Queries
{
    public class GetPurchaseByIdQueryTests
    {
        [Fact]
        public async void GetPurchaseByIdShouldWorks_WhenAPurchaseIsFoundById()
        {
            //Arrange
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockLogger = new Mock<ILogger>();
            var newGuid = Guid.NewGuid();
            Purchase purchase1 = new Purchase()
            {
                Amount = 10,
                CarId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                Id = Guid.NewGuid()
            };

            mockPurchaseRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(purchase1);

            var handler = new GetPurchaseByIdQuery.GetPurchaseByIdQueryHandler(mockPurchaseRepository.Object);
            var query = new GetPurchaseByIdQuery(newGuid);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(purchase1, result.Value);
            mockPurchaseRepository.Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetPurchaseByIdShouldFails_WhenAPurchaseIsNotFoundById()
        {
            //Arrange
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockLogger = new Mock<ILogger>();
            var newGuid = Guid.NewGuid();

            mockPurchaseRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(Maybe<Purchase>.None);

            var handler = new GetPurchaseByIdQuery.GetPurchaseByIdQueryHandler(mockPurchaseRepository.Object);
            var query = new GetPurchaseByIdQuery(newGuid);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            mockPurchaseRepository.Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Once);
        }
    }
}