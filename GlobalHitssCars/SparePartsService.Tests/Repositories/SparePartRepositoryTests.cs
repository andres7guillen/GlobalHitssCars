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
            var SparePartExpected = SparePart.Build("Spare test",
                "Brand spare test",
                "Brand car test",
                2000,
                "Reference test",
                true,
                10);
            //Act
            var result = await repository.Create(SparePartExpected.Value);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void DeleteSparePartShouldWorks()
        {
            //Arrange
            var context = ApplicationSparePartDbContextInMemory.Get();
            var repository = new SparePartRepository(context);
            var sparePartExpected = SparePart.Build("Spare test",
                "Brand spare test",
                "Brand car test",
                2000,
                "Reference test",
                true,
                10);

            //Act
            await repository.Create(sparePartExpected.Value);
            var resultDeletedOk = await repository.DeleteSparePart(sparePartExpected.Value.Id);
            var resultGetById = await repository.GetSparePartById(sparePartExpected.Value.Id);

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
            var sparePartExpected = SparePart.Build("Spare test",
                "Brand spare test",
                "Brand car test",
                2000,
                "Reference test",
                true,
                10);
            await repository.Create(sparePartExpected.Value);

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
            var sparePartExpected = SparePart.Build("Spare test",
                "Brand spare test",
                "Brand car test",
                2000,
                "Reference test",
                true,
                10);

            //Act
            await repository.Create(sparePartExpected.Value);
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
            var sparePartExpected = SparePart.Build("Spare test",
                "Brand spare test",
                "Brand car test",
                2000,
                "Reference test",
                true,
                10);

            //Act
            await repository.Create(sparePartExpected.Value);
            var SparePartById = await repository.GetSparePartById(sparePartExpected.Value.Id);

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

            var sparePartExpected = SparePart.Build("Spare test",
                "Brand spare test",
                "Brand car test",
                2000,
                "Reference test",
                true,
                10);

            //Act
            await repository.Create(sparePartExpected.Value);
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

            var sparePartExpected = SparePart.Build("Spare test",
                "Brand spare test",
                "Brand car test",
                2000,
                "Reference test",
                true,
                10);
            var filter = new GetSparePartByFilterDTO()
            {
                BrandCar = "Brand car test",
                BrandSpare = "Brand spare test"
            };

            //Act
            await repository.Create(sparePartExpected.Value);
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

            var sparePartExpected = SparePart.Build("Spare test",
                "Brand spare test",
                "Brand car test",
                2000,
                "Reference test",
                true,
                10);
            var filter = new GetSparePartByFilterDTO()
            {
                BrandCar = "BrandCarTest",
                BrandSpare = "Brand spare test"
            };

            //Act
            await repository.Create(sparePartExpected.Value);
            var SparePartByFilter = await repository.GetSparePartsByFilter(filter);

            //Assert
            Assert.True(SparePartByFilter.Count() == 0);
        }

        [Fact]
        public async void UpdateSparePartShouldWorks()
        {
            //Arrange
            var context = ApplicationSparePartDbContextInMemory.Get();
            var repository = new SparePartRepository(context);
            var newStock = -10;
            var sparePartExpected = SparePart.Build("Spare test",
                "Brand spare test",
                "Brand car test",
                2000,
                "Reference test",
                true,
                10);
            //Act
            await repository.Create(sparePartExpected.Value);
            var SparePartById = await repository.GetSparePartById(sparePartExpected.Value.Id);
            var SparePartToUpdate = SparePartById.Value;
            SparePartToUpdate.Stock = newStock;
            await repository.UpdatateSpare(SparePartToUpdate);

            var SparePartByIdUpdated = await repository.GetSparePartById(sparePartExpected.Value.Id);

            //Assert
            Assert.True(SparePartToUpdate.Stock == newStock);
        }
    }
}
