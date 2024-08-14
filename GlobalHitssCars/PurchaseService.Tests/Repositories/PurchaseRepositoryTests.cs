using PurchaseService.Tests.Config;
using PurchaseServiceDomain.Entities;
using PurchaseServiceInfrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var PurchaseTest = new Purchase()
            {
                Id = idPurchase,
                Amount = 0,
                CarId = Guid.NewGuid(),
                ClientId = Guid.NewGuid()
            };
            //Act
            var result = await repository.Create(PurchaseTest);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void DeletePurchaseShouldWorks()
        {
            //Arrange
            var context = ApplicationPurchaseDbContextInMemory.Get();
            var repository = new PurchaseRepository(context);
            var idPurchase = Guid.NewGuid();
            var PurchaseTest = new Purchase()
            {
                Id = idPurchase,
                Amount = 0,
                CarId = Guid.NewGuid(),
                ClientId = Guid.NewGuid()
            };

            //Act
            await repository.Create(PurchaseTest);
            var resultDeletedOk = await repository.Delete(idPurchase);
            var resultGetById = await repository.GetById(idPurchase);

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
            var idPurchase = Guid.NewGuid();
            var PurchaseTest = new Purchase()
            {
                Id = idPurchase,
                Amount = 0,
                CarId = Guid.NewGuid(),
                ClientId = Guid.NewGuid()
            };
            await repository.Create(PurchaseTest);

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
            var PurchaseTest = new Purchase()
            {
                Id = idPurchase,
                Amount = 0,
                CarId = Guid.NewGuid(),
                ClientId = Guid.NewGuid()
            };

            //Act
            await repository.Create(PurchaseTest);
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
            var idPurchase = Guid.NewGuid();
            var PurchaseTest = new Purchase()
            {
                Id = idPurchase,
                Amount = 0,
                CarId = Guid.NewGuid(),
                ClientId = Guid.NewGuid()
            };

            //Act
            await repository.Create(PurchaseTest);
            var PurchaseById = await repository.GetById(idPurchase);

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
            var PurchaseTest = new Purchase()
            {
                Id = idPurchase,
                Amount = 0,
                CarId = Guid.NewGuid(),
                ClientId = Guid.NewGuid()
            };

            //Act
            await repository.Create(PurchaseTest);
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
            var idPurchase = Guid.NewGuid();
            var PurchaseTest = new Purchase()
            {
                Id = idPurchase,
                Amount = 0,
                CarId = Guid.NewGuid(),
                ClientId = Guid.NewGuid()
            };
            //Act
            await repository.Create(PurchaseTest);
            var PurchaseById = await repository.GetById(idPurchase);
            var PurchaseToUpdate = PurchaseById.Value;
            PurchaseToUpdate.Amount = newAmount;
            await repository.Update(PurchaseToUpdate);

            var PurchaseByIdUpdated = await repository.GetById(idPurchase);

            //Assert
            Assert.True(PurchaseToUpdate.Amount == newAmount);
        }
    }
}
