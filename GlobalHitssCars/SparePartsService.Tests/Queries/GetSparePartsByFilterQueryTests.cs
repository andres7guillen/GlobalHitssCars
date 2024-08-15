using Moq;
using SparePartServiceApplication.Queries;
using SparePartsServiceDomain.DTOs;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Exceptions;
using SparePartsServiceDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsService.Tests.Queries
{
    public class GetSparePartsByFilterQueryTests
    {
        [Fact]
        public async void GetSparePartsByFilterShouldWorks_WhenSparePartsAreFound()
        {
            //Arrange
            List<SparePart> list = new List<SparePart>();
            var SparePartExpected1 = SparePart.Build("Spare test",
                "Brand spare test",
                "Brand car test",
                2000,
                "Reference test",
                true,
                10);
            var SparePartExpected2 = SparePart.Build("Spare test",
                "Brand spare test",
                "Brand car test",
                2000,
                "Reference test",
                true,
                10);
            list.Add(SparePartExpected1.Value);
            list.Add(SparePartExpected2.Value);
            var filter = new GetSparePartByFilterDTO()
            {
                BrandCar = "Brand car test",
                BrandSpare = "Brans car spare test"
            };

            var mockSparePartRepository = new Mock<ISparePartRepository>();
            mockSparePartRepository.Setup(repo => repo.GetSparePartsByFilter(It.IsAny<GetSparePartByFilterDTO>()))
                .ReturnsAsync(list);
            var query = new GetSparePartsByFilterQuery(filter);
            var handler = new GetSparePartsByFilterQuery.GetSparePartsByFilterHandler(mockSparePartRepository.Object);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);
            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(list, result.Value);
            mockSparePartRepository.Verify(repo => repo.GetSparePartsByFilter(It.IsAny<GetSparePartByFilterDTO>()), Times.Once);
        }

        [Fact]
        public async void GetSparePartsByFilterShouldFails_WhenNoSparePartsAreFound()
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
