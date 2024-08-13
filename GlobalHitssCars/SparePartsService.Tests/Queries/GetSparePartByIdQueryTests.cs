using Common.Logging.Interfaces;
using CSharpFunctionalExtensions;
using Moq;
using SparePartServiceApplication.Queries;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Repositories;

namespace SparePartsService.Tests.Queries
{
    public class GetSparePartByIdQueryTests
    {
        [Fact]
        public async void GetSparePartByIdShouldWorks_WhenASparePartIsFoundById()
        {
            //Arrange
            var mockSparePartRepository = new Mock<ISparePartRepository>();
            var mockLogger = new Mock<ILogger>();
            var newGuid = Guid.NewGuid();
            SparePart SparePart1 = new SparePart()
            {
                BrandCar = "Brand car test",
                BrandSpare = "Brand spare test",
                Id = newGuid,
                IsInStock = true,
                ModelCar = 2000,
                ReferenceCar = "Reference test",
                SpareName = "Spare test",
                Stock = 0
            };

            mockSparePartRepository.Setup(repo => repo.GetSparePartById(It.IsAny<Guid>()))
                .ReturnsAsync(SparePart1);

            var handler = new GetSparePartByIdQuery.GetSparePartByIdQueryHandler(mockSparePartRepository.Object);
            var query = new GetSparePartByIdQuery(newGuid);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(SparePart1, result.Value);
            mockSparePartRepository.Verify(repo => repo.GetSparePartById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetSparePartByIdShouldFails_WhenASparePartIsNotFoundById()
        {
            //Arrange
            var mockSparePartRepository = new Mock<ISparePartRepository>();
            var mockLogger = new Mock<ILogger>();
            var newGuid = Guid.NewGuid();

            mockSparePartRepository.Setup(repo => repo.GetSparePartById(It.IsAny<Guid>()))
                .ReturnsAsync(Maybe<SparePart>.None);

            var handler = new GetSparePartByIdQuery.GetSparePartByIdQueryHandler(mockSparePartRepository.Object);
            var query = new GetSparePartByIdQuery(newGuid);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            mockSparePartRepository.Verify(repo => repo.GetSparePartById(It.IsAny<Guid>()), Times.Once);
        }
    }
}
