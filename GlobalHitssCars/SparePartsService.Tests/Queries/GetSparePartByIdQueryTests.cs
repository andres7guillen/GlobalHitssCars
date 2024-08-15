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
            var SparePartExpected = SparePart.Build("Spare test",
                "Brand spare test",
                "Brand car test",
                2000,
                "Reference test",
                true,
                10);

            mockSparePartRepository.Setup(repo => repo.GetSparePartById(It.IsAny<Guid>()))
                .ReturnsAsync(SparePartExpected.Value);

            var handler = new GetSparePartByIdQuery.GetSparePartByIdQueryHandler(mockSparePartRepository.Object);
            var query = new GetSparePartByIdQuery(newGuid);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(SparePartExpected.Value, result.Value);
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
