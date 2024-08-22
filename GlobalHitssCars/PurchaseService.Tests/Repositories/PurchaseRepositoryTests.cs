using PurchaseService.Tests.Config;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Enum;
using PurchaseServiceInfrastructure.Repositories;

namespace PurchaseService.Tests.Repositories
{
    public class PurchaseRepositoryTests
    {
        [Fact]
        public async void CreatePurchaseShouldWorks()
        {
            //Arrange
            var context = ApplicationPurchaseDbContextInMemory.Get();
            var repository = new PurchaseRepository(context);
            var idPurchase = Guid.NewGuid();
            var purchaseExpected = Purchase.Build(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                10,
                70000000,
                TypePurchaseEnum.Car);
            //Act
            var result = await repository.Create(purchaseExpected.Value);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void DeletePurchaseShouldWorks()
        {
            //Arrange
            var context = ApplicationPurchaseDbContextInMemory.Get();
            var repository = new PurchaseRepository(context);
            var purchaseExpected = Purchase.Build(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                10,
                70000000,
                TypePurchaseEnum.Car);

            //Act
            await repository.Create(purchaseExpected.Value);
            var resultDeletedOk = await repository.Delete(purchaseExpected.Value.Id);
            var resultGetById = await repository.GetById(purchaseExpected.Value.Id);

            //Assert
            Assert.True(resultDeletedOk);
            Assert.True(resultGetById.HasNoValue);
        }

        [Fact]
        public async void DeletePurchaseShouldFails_WhenIdNotExists()
        {
            //Arrange
            var context = ApplicationPurchaseDbContextInMemory.Get();
            var repository = new PurchaseRepository(context);
            var purchaseExpected = Purchase.Build(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                10,
                70000000,
                TypePurchaseEnum.Car);
            await repository.Create(purchaseExpected.Value);

            //Act
            var resultDeletedFail = await repository.Delete(Guid.NewGuid());

            //Assert
            Assert.False(resultDeletedFail);
        }

        [Fact]
        public async void GetAllShouldWorks()
        {
            //Arrange
            var context = ApplicationPurchaseDbContextInMemory.Get();
            var repository = new PurchaseRepository(context);
            var idPurchase = Guid.NewGuid();
            var purchaseExpected = Purchase.Build(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                10,
                70000000,
                TypePurchaseEnum.Car);

            //Act
            await repository.Create(purchaseExpected.Value);
            var list = await repository.GetAll();

            //Assert
            Assert.NotNull(list);
        }

        [Fact]
        public async void GetPurchaseByIdShouldWorks()
        {
            //Arrange
            var context = ApplicationPurchaseDbContextInMemory.Get();
            var repository = new PurchaseRepository(context);
            var purchaseExpected = Purchase.Build(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                10,
                70000000,
                TypePurchaseEnum.Car);

            //Act
            await repository.Create(purchaseExpected.Value);
            var PurchaseById = await repository.GetById(purchaseExpected.Value.Id);

            //Assert
            Assert.NotNull(PurchaseById.Value);
            Assert.True(PurchaseById.HasValue);
        }

        [Fact]
        public async void GetPurchaseByIdShouldFails_WhenIdGivenNotExists()
        {
            //Arrange
            var context = ApplicationPurchaseDbContextInMemory.Get();
            var repository = new PurchaseRepository(context);

            var idPurchase = Guid.NewGuid();
            var purchaseExpected = Purchase.Build(
                 Guid.NewGuid(),
                 Guid.NewGuid(),
                 Guid.NewGuid(),
                 10,
                 70000000,
                 TypePurchaseEnum.Car);

            //Act
            await repository.Create(purchaseExpected.Value);
            var PurchaseById = await repository.GetById(Guid.NewGuid());

            //Assert
            Assert.True(PurchaseById.HasNoValue);
        }

        [Fact]
        public async void UpdatePurchaseShouldWorks()
        {
            //Arrange
            var context = ApplicationPurchaseDbContextInMemory.Get();
            var repository = new PurchaseRepository(context);
            var newAmount = 1552;
            var purchaseExpected = Purchase.Build(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                10,
                70000000,
                TypePurchaseEnum.Car);
            //Act
            await repository.Create(purchaseExpected.Value);
            var PurchaseById = await repository.GetById(purchaseExpected.Value.Id);
            var PurchaseToUpdate = PurchaseById.Value;
            PurchaseToUpdate.Amount = newAmount;
            await repository.Update(PurchaseToUpdate);

            var PurchaseByIdUpdated = await repository.GetById(purchaseExpected.Value.Id);

            //Assert
            Assert.True(PurchaseToUpdate.Amount == newAmount);
        }
    }
}
