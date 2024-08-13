using Moq;
using SparePartServiceApplication.Queries;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Exceptions;
using SparePartsServiceDomain.Repositories;

namespace SparePartsService.Tests.Queries
{

    public class GetAllSparePartsQueryTests
    {
        [Fact]
        public async void GetAllSparePartsShouldWorks_WhenSparePartsAreFound()
        {
            //Arrange
            List<SparePart> list = new List<SparePart>();
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
            var newGuid2 = Guid.NewGuid();
            SparePart SparePart2 = new SparePart()
            {
                BrandCar = "Brand car test2",
                BrandSpare = "Brand spare test2",
                Id = newGuid2,
                IsInStock = true,
                ModelCar = 2000,
                ReferenceCar = "Reference test2",
                SpareName = "Spare test2",
                Stock = 1
            };
            list.Add(SparePart1);
            list.Add(SparePart2);

            var mockSparePartRepository = new Mock<ISparePartRepository>();
            mockSparePartRepository.Setup(repo => repo.GetAllSpareParts(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(list);
            var query = new GetAllSparePartsQuery(0, 10);
            var handler = new GetAllSparePartsQuery.GetAllSparePartsQueryHandler(mockSparePartRepository.Object);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);
            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(list, result.Value);
            mockSparePartRepository.Verify(repo => repo.GetAllSpareParts(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void GetAllSparePartsShouldFails_WhenNoSparePartsAreFound()
        {
            //Arrange
            var mockSparePartRepository = new Mock<ISparePartRepository>();
            List<SparePart> list = new List<SparePart>();
            mockSparePartRepository.Setup(repo => repo.GetAllSpareParts(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(list);

            var query = new GetAllSparePartsQuery(0, 10);
            var handler = new GetAllSparePartsQuery.GetAllSparePartsQueryHandler(mockSparePartRepository.Object);
            //Act
            var result = await handler.Handle(query, CancellationToken.None);
            //Assert
            Assert.Equal(SparePartContextExceptionEnum.NoSparePartsFound.GetErrorMessage(), result.Error);
            mockSparePartRepository.Verify(repo => repo.GetAllSpareParts(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

    }
}
