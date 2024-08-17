using Moq;
using PurchaseApplication.Queries;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Exceptions;
using PurchaseServiceDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseService.Tests.Queries
{
    public class GetAllPurchasesQueryTests
    {
        [Fact]
        public async void GetAllPurchasesShouldWorks_WhenPurchasesAreFound()
        {
            //Arrange
            List<Purchase> list = new List<Purchase>();
            var purchaseExpected1 = Purchase.Build(
                Guid.NewGuid(),
                Guid.NewGuid(),
                70000000);
            var purchaseExpected2 = Purchase.Build(
                Guid.NewGuid(),
                Guid.NewGuid(),
                78000000);
            list.Add(purchaseExpected1.Value);
            list.Add(purchaseExpected2.Value);

            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            mockPurchaseRepository.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Tuple<int, IEnumerable<Purchase>>(list.Count(), list));
            var query = new GetAllPurchasesQuery(0, 10);
            var handler = new GetAllPurchasesQuery.GetAllPurchasesQueryHandler(mockPurchaseRepository.Object);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);
            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(list, result.Value.Item2);
            mockPurchaseRepository.Verify(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void GetAllPurchasesShouldFails_WhenNoPurchasesAreFound()
        {
            //Arrange
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            List<Purchase> list = new List<Purchase>();
            mockPurchaseRepository.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Tuple<int, IEnumerable<Purchase>>(list.Count(), list));

            var query = new GetAllPurchasesQuery(0, 10);
            var handler = new GetAllPurchasesQuery.GetAllPurchasesQueryHandler(mockPurchaseRepository.Object);
            //Act
            var result = await handler.Handle(query, CancellationToken.None);
            //Assert
            Assert.Equal(PurchaseContextExceptionEnum.NoPurchasesFound.GetErrorMessage(), result.Error);
            mockPurchaseRepository.Verify(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

    }
}
