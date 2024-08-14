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
            var idCar = Guid.NewGuid();
            var carTest = new Car()
            {
                Id = idCar,
                Brand = "Brand test",
                Colour = "Colour test",
                LicensePlate = "ABC123",
                Model = 0000,
                Reference = "Reference test"
            };
            //Act
            var result = await repository.Create(carTest);

            //Assert
            Assert.NotNull(result);            
        }

        [Fact]
        public async void DeleteCarShouldWorks() 
        {
            //Arrange
            var context = ApplicationCarDbContextInMemory.Get();
            var repository = new CarRepository(context);
            var carId = Guid.NewGuid();
            var carTest = new Car()
            {   
                Id = carId,
                Brand = "Brand test",
                Colour = "Colour test",
                LicensePlate = "ABC123",
                Model = 0000,
                Reference = "Reference test"
            };

            //Act
            await repository.Create(carTest);
            var resultDeletedOk = await repository.Delete(carId);
            var resultGetById = await repository.GetById(carId);

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
            var carId = Guid.NewGuid();
            var carTest = new Car()
            {
                Id = carId,
                Brand = "Brand test",
                Colour = "Colour test",
                LicensePlate = "ABC123",
                Model = 0000,
                Reference = "Reference test"
            };
            await repository.Create(carTest);

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
            var carId = Guid.NewGuid();
            var carTest = new Car()
            {
                Id = carId,
                Brand = "Brand test",
                Colour = "Colour test",
                LicensePlate = "ABC123",
                Model = 0000,
                Reference = "Reference test"
            };

            //Act
            await repository.Create(carTest);
            var list = await repository.GetAll();

            //Assert
            Assert.NotNull(list);
        }

        [Fact]
        public async void GetCarByIdShouldWorks()
        {
            //Arrange
            var context = ApplicationCarDbContextInMemory.Get();
            var idCar = Guid.NewGuid();
            var repository = new CarRepository(context);
            
            var carTest = new Car()
            {
                Id = idCar,
                Brand = "Brand test",
                Colour = "Colour test",
                LicensePlate = "ABC123",
                Model = 0000,
                Reference = "Reference test"
            };

            //Act
            await repository.Create(carTest);
            var carById = await repository.GetById(idCar);

            //Assert
            Assert.NotNull(carById.Value);
            Assert.True(carById.HasValue);
        }

        [Fact]
        public async void GetCarByIdShouldFails_WhenIdGivenNotExists()
        {
            //Arrange
            var context = ApplicationCarDbContextInMemory.Get();
            var idCar = Guid.NewGuid();
            var repository = new CarRepository(context);

            var carTest = new Car()
            {
                Id = idCar,
                Brand = "Brand test",
                Colour = "Colour test",
                LicensePlate = "ABC123",
                Model = 0000,
                Reference = "Reference test"
            };

            //Act
            await repository.Create(carTest);
            var carById = await repository.GetById(Guid.NewGuid());

            //Assert
            Assert.True(carById.HasNoValue);
        }

        [Fact]
        public async void GetCarByFilterShouldWorks_WhenCarsAreFound() 
        {
            //Arrange
            var context = ApplicationCarDbContextInMemory.Get();
            var idCar = Guid.NewGuid();
            var repository = new CarRepository(context);

            var carTest = new Car()
            {
                Id = idCar,
                Brand = "Brand test",
                Colour = "Colour test",
                LicensePlate = "ABC123",
                Model = 0000,
                Reference = "Reference test"
            };

            var filter = new CarByFilterDTO()
            {
                Brand = "Brand test"
            };

            //Act
            await repository.Create(carTest);
            var resultCarByFilter = await repository.GetCarByFilter(filter);

            //Assert
            Assert.NotNull(resultCarByFilter);
        }

        [Fact]
        public async void GetCarByFilterShouldWorks_WhenCarsAreNotFound()
        {
            //Arrange
            var context = ApplicationCarDbContextInMemory.Get();
            var idCar = Guid.NewGuid();
            var repository = new CarRepository(context);

            var carTest = new Car()
            {
                Id = idCar,
                Brand = "Brand test",
                Colour = "Colour test",
                LicensePlate = "ABC123",
                Model = 0000,
                Reference = "Reference test"
            };

            var filter = new CarByFilterDTO()
            {
                Brand = "Brand filter"
            };

            //Act
            await repository.Create(carTest);
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
            var idCar = Guid.NewGuid();
            var newBrand = "NewBrandUpdated";
            var carTest = new Car()
            {
                Id = idCar,
                Brand = "Brand test",
                Colour = "Colour test",
                LicensePlate = "ABC123",
                Model = 0000,
                Reference = "Reference test"
            };
            //Act
            await repository.Create(carTest);
            var carById = await repository.GetById(idCar);
            var carToUpdate = carById.Value;
            carToUpdate.Brand = newBrand;
            await repository.Update(carToUpdate);

            var carByIdUpdated = await repository.GetById(idCar);

            //Assert
            Assert.True(carToUpdate.Brand == newBrand);
        }

    }
}
