using SparePartsService.Tests.Config;
using SparePartsServiceDomain.DTOs;
using SparePartsServiceDomain.Entities;
using SparePartsServiceInfrastructure.Repositories;

namespace SparePartsService.Tests.Repositories
{
    public class SparePartRepositoryTests
    {
        [Fact]
        public async void CreateSparePartShouldWorks()
        {
            //Arrange
            var context = ApplicationSparePartDbContextInMemory.Get();
            var repository = new SparePartRepository(context);
            var idSparePart = Guid.NewGuid();
            var SparePartTest = new SparePart()
            {
                Id = idSparePart,
                BrandCar = "BrandCarTest",
                BrandSpare = "BransSpareTest",
                IsInStock = true,
                ModelCar = 2000,
                ReferenceCar = "ReferenceCarTest",
                SpareName = "SpareNameTest",
                Stock = 10
            };
            //Act
            var result = await repository.Create(SparePartTest);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void DeleteSparePartShouldWorks()
        {
            //Arrange
            var context = ApplicationSparePartDbContextInMemory.Get();
            var repository = new SparePartRepository(context);
            var idSparePart = Guid.NewGuid();
            var SparePartTest = new SparePart()
            {
                Id = idSparePart,
                BrandCar = "BrandCarTest",
                BrandSpare = "BransSpareTest",
                IsInStock = true,
                ModelCar = 2000,
                ReferenceCar = "ReferenceCarTest",
                SpareName = "SpareNameTest",
                Stock = 10
            };

            //Act
            await repository.Create(SparePartTest);
            var resultDeletedOk = await repository.DeleteSparePart(idSparePart);
            var resultGetById = await repository.GetSparePartById(idSparePart);

            //Assert
            Assert.True(resultDeletedOk);
            Assert.True(resultGetById.HasNoValue);
        }

        [Fact]
        public async void DeleteSparePartShouldFails_WhenIdNotExists()
        {
            //Arrange
            var context = ApplicationSparePartDbContextInMemory.Get();
            var repository = new SparePartRepository(context);
            var idSparePart = Guid.NewGuid();
            var SparePartTest = new SparePart()
            {
                Id = idSparePart,
                BrandCar = "BrandCarTest",
                BrandSpare = "BransSpareTest",
                IsInStock = true,
                ModelCar = 2000,
                ReferenceCar = "ReferenceCarTest",
                SpareName = "SpareNameTest",
                Stock = 10
            };
            await repository.Create(SparePartTest);

            //Act
            var resultDeletedFail = await repository.DeleteSparePart(Guid.NewGuid());

            //Assert
            Assert.False(resultDeletedFail);
        }

        [Fact]
        public async void GetAllShouldWorks()
        {
            //Arrange
            var context = ApplicationSparePartDbContextInMemory.Get();
            var repository = new SparePartRepository(context);
            var idSparePart = Guid.NewGuid();
            var SparePartTest = new SparePart()
            {
                Id = idSparePart,
                BrandCar = "BrandCarTest",
                BrandSpare = "BransSpareTest",
                IsInStock = true,
                ModelCar = 2000,
                ReferenceCar = "ReferenceCarTest",
                SpareName = "SpareNameTest",
                Stock = 10
            };

            //Act
            await repository.Create(SparePartTest);
            var list = await repository.GetAllSpareParts();

            //Assert
            Assert.NotNull(list);
        }

        [Fact]
        public async void GetSparePartByIdShouldWorks()
        {
            //Arrange
            var context = ApplicationSparePartDbContextInMemory.Get();
            var repository = new SparePartRepository(context);
            var idSparePart = Guid.NewGuid();
            var SparePartTest = new SparePart()
            {
                Id = idSparePart,
                BrandCar = "BrandCarTest",
                BrandSpare = "BransSpareTest",
                IsInStock = true,
                ModelCar = 2000,
                ReferenceCar = "ReferenceCarTest",
                SpareName = "SpareNameTest",
                Stock = 10
            };

            //Act
            await repository.Create(SparePartTest);
            var SparePartById = await repository.GetSparePartById(idSparePart);

            //Assert
            Assert.NotNull(SparePartById.Value);
            Assert.True(SparePartById.HasValue);
        }

        [Fact]
        public async void GetSparePartByIdShouldFails_WhenIdGivenNotExists()
        {
            //Arrange
            var context = ApplicationSparePartDbContextInMemory.Get();
            var repository = new SparePartRepository(context);

            var idSparePart = Guid.NewGuid();
            var SparePartTest = new SparePart()
            {
                Id = idSparePart,
                BrandCar = "BrandCarTest",
                BrandSpare = "BransSpareTest",
                IsInStock = true,
                ModelCar = 2000,
                ReferenceCar = "ReferenceCarTest",
                SpareName = "SpareNameTest",
                Stock = 10
            };

            //Act
            await repository.Create(SparePartTest);
            var SparePartById = await repository.GetSparePartById(Guid.NewGuid());

            //Assert
            Assert.True(SparePartById.HasNoValue);
        }

        [Fact]
        public async void GetSparePartsByFilterShouldWorks() 
        {
            //Arrange
            var context = ApplicationSparePartDbContextInMemory.Get();
            var repository = new SparePartRepository(context);

            var idSparePart = Guid.NewGuid();
            var SparePartTest = new SparePart()
            {
                Id = idSparePart,
                BrandCar = "BrandCarTest",
                BrandSpare = "BransSpareTest",
                IsInStock = true,
                ModelCar = 2000,
                ReferenceCar = "ReferenceCarTest",
                SpareName = "SpareNameTest",
                Stock = 10
            };
            var filter = new GetSparePartByFilterDTO()
            {
                BrandCar = "BrandCarTest",
                BrandSpare = "BransSpareTest"
            };

            //Act
            await repository.Create(SparePartTest);
            var SparePartByFilter = await repository.GetSparePartsByFilter(filter);

            //Assert
            Assert.True(SparePartByFilter.Count() >= 1);
        }

        [Fact]
        public async void GetSparePartsByFilterShouldWorks_WhenSparesAreNotFound()
        {
            //Arrange
            var context = ApplicationSparePartDbContextInMemory.Get();
            var repository = new SparePartRepository(context);

            var idSparePart = Guid.NewGuid();
            var SparePartTest = new SparePart()
            {
                Id = idSparePart,
                BrandCar = "BrandCarTest",
                BrandSpare = "BransSpareTest",
                IsInStock = true,
                ModelCar = 2000,
                ReferenceCar = "ReferenceCarTest",
                SpareName = "SpareNameTest",
                Stock = 10
            };
            var filter = new GetSparePartByFilterDTO()
            {
                BrandCar = "BrandCar",
                BrandSpare = "BransSpare"
            };

            //Act
            await repository.Create(SparePartTest);
            var SparePartByFilter = await repository.GetSparePartsByFilter(filter);

            //Assert
            Assert.True(SparePartByFilter.Count() == 0);
        }

        [Fact]
        public async void LessStockShouldWorks() 
        {
            //Arrange
            var context = ApplicationSparePartDbContextInMemory.Get();
            var repository = new SparePartRepository(context);

            var idSparePart = Guid.NewGuid();
            var SparePartTest = new SparePart()
            {
                Id = idSparePart,
                BrandCar = "BrandCarTest",
                BrandSpare = "BransSpareTest",
                IsInStock = true,
                ModelCar = 2000,
                ReferenceCar = "ReferenceCarTest",
                SpareName = "SpareNameTest",
                Stock = 10
            };

            //Act
            await repository.Create(SparePartTest);
            var SpareStockSub = await repository.LessStock(idSparePart,1);

            //Assert
            Assert.True(SpareStockSub);
        }

        [Fact]
        public async void AddStockShouldWorks() 
        {
            //Arrange
            var context = ApplicationSparePartDbContextInMemory.Get();
            var repository = new SparePartRepository(context);

            var idSparePart = Guid.NewGuid();
            var SparePartTest = new SparePart()
            {
                Id = idSparePart,
                BrandCar = "BrandCarTest",
                BrandSpare = "BransSpareTest",
                IsInStock = true,
                ModelCar = 2000,
                ReferenceCar = "ReferenceCarTest",
                SpareName = "SpareNameTest",
                Stock = 10
            };

            //Act
            await repository.Create(SparePartTest);
            var SpareStockAdded = await repository.AddStock(idSparePart, 1);

            //Assert
            Assert.True(SpareStockAdded);
        }

        [Fact]
        public async void UpdateSparePartShouldWorks()
        {
            //Arrange
            var context = ApplicationSparePartDbContextInMemory.Get();
            var repository = new SparePartRepository(context);
            var newStock = -10;
            var idSparePart = Guid.NewGuid();
            var SparePartTest = new SparePart()
            {
                Id = idSparePart,
                BrandCar = "BrandCarTest",
                BrandSpare = "BransSpareTest",
                IsInStock = true,
                ModelCar = 2000,
                ReferenceCar = "ReferenceCarTest",
                SpareName = "SpareNameTest",
                Stock = 10
            };
            //Act
            await repository.Create(SparePartTest);
            var SparePartById = await repository.GetSparePartById(idSparePart);
            var SparePartToUpdate = SparePartById.Value;
            SparePartToUpdate.Stock = newStock;
            await repository.UpdatateSpare(SparePartToUpdate);

            var SparePartByIdUpdated = await repository.GetSparePartById(idSparePart);

            //Assert
            Assert.True(SparePartToUpdate.Stock == newStock);
        }
    }
}
