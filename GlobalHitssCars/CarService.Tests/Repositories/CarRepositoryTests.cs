using CarService.Tests.Config;
using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;
using CarServiceInfrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Tests.Repositories
{
    public class CarRepositoryTests
    {
        [Fact]
        public async void CreateCarShouldWorks() 
        {
            //Arrange
            var context = ApplicationCarDbContextInMemory.Get();
            var repository = new CarRepository(context);
            var car1 = Car.Build("Brand test1",
                2021,
                "Reference test1",
                "Colour test1",
                "ABC123"
                );
            //Act
            var result = await repository.Create(car1.Value);

            //Assert
            Assert.NotNull(result);            
        }

        [Fact]
        public async void DeleteCarShouldWorks() 
        {
            //Arrange
            var context = ApplicationCarDbContextInMemory.Get();
            var repository = new CarRepository(context);
            var carTest = Car.Build("Brand test1",
                2024,
                "Reference test1",
                "Colour test1",
                "ABC123"
                );

            //Act
            await repository.Create(carTest.Value);
            var resultDeletedOk = await repository.Delete(carTest.Value.Id);
            var resultGetById = await repository.GetById(carTest.Value.Id);

            //Assert
            Assert.True(resultDeletedOk);
            Assert.True(resultGetById.HasNoValue);
        }

        [Fact]
        public async void DeleteCarShouldFails_WhenIdNotExists()
        {
            //Arrange
            var context = ApplicationCarDbContextInMemory.Get();
            var repository = new CarRepository(context);
            var carTest = Car.Build("Brand test1",
                 2024,
                 "Reference test1",
                 "Colour test1",
                 "ABC123"
                 );
            await repository.Create(carTest.Value);

            //Act
            var resultDeletedFail = await repository.Delete(Guid.NewGuid());

            //Assert
            Assert.False(resultDeletedFail);
        }

        [Fact]
        public async void GetAllShouldWorks() 
        {
            //Arrange
            var context = ApplicationCarDbContextInMemory.Get();
            var repository = new CarRepository(context);
            var carTest = Car.Build("Brand test1",
                2021,
                "Reference test1",
                "Colour test1",
                "ABC123"
                );

            //Act
            await repository.Create(carTest.Value);
            var list = await repository.GetAll();

            //Assert
            Assert.NotNull(list);
        }

        [Fact]
        public async void GetCarByIdShouldWorks()
        {
            //Arrange
            var context = ApplicationCarDbContextInMemory.Get();
            var repository = new CarRepository(context);

            var carTest = Car.Build("Brand test1",
                2021,
                "Reference test1",
                "Colour test1",
                "ABC123"
                );

            //Act
            await repository.Create(carTest.Value);
            var carById = await repository.GetById(carTest.Value.Id);

            //Assert
            Assert.NotNull(carById.Value);
            Assert.True(carById.HasValue);
        }

        [Fact]
        public async void GetCarByIdShouldFails_WhenIdGivenNotExists()
        {
            //Arrange
            var context = ApplicationCarDbContextInMemory.Get();
            var repository = new CarRepository(context);

            var carTest = Car.Build("Brand test1",
                2021,
                "Reference test1",
                "Colour test1",
                "ABC123"
                );

            //Act
            await repository.Create(carTest.Value);
            var carById = await repository.GetById(Guid.NewGuid());

            //Assert
            Assert.True(carById.HasNoValue);
        }

        [Fact]
        public async void GetCarByFilterShouldWorks_WhenCarsAreFound() 
        {
            //Arrange
            var context = ApplicationCarDbContextInMemory.Get();
            var repository = new CarRepository(context);

            var carTest = Car.Build("Brand test1",
                2021,
                "Reference test1",
                "Colour test1",
                "ABC123"
                );

            var filter = new CarByFilterDTO()
            {
                Brand = "Brand test"
            };

            //Act
            await repository.Create(carTest.Value);
            var resultCarByFilter = await repository.GetCarByFilter(filter);

            //Assert
            Assert.NotNull(resultCarByFilter);
        }

        [Fact]
        public async void GetCarByFilterShouldWorks_WhenCarsAreNotFound()
        {
            //Arrange
            var context = ApplicationCarDbContextInMemory.Get();
            var repository = new CarRepository(context);

            var carTest = Car.Build("Brand test1",
                2021,
                "Reference test1",
                "Colour test1",
                "ABC123"
                );

            var filter = new CarByFilterDTO()
            {
                Brand = "Brand filter"
            };

            //Act
            await repository.Create(carTest.Value);
            var resultCarByFilter = await repository.GetCarByFilter(filter);

            //Assert
            Assert.True(resultCarByFilter.Count() == 0);
        }

        [Fact]
        public async void UpdateCarShouldWorks()
        {
            //Arrange
            var context = ApplicationCarDbContextInMemory.Get();
            var repository = new CarRepository(context);
            var newBrand = "NewBrandUpdated";
            var carTest = Car.Build("Brand test1",
                2021,
                "Reference test1",
                "Colour test1",
                "ABC123"
                );
            //Act
            await repository.Create(carTest.Value);
            var carById = await repository.GetById(carTest.Value.Id);
            var carToUpdate = carById.Value;
            carToUpdate.Brand = newBrand;
            await repository.Update(carToUpdate);

            var carByIdUpdated = await repository.GetById(carTest.Value.Id);

            //Assert
            Assert.True(carToUpdate.Brand == newBrand);
        }

    }
}
