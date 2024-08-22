using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;
using CarServiceDomain.Exceptions;
using CarServiceDomain.Repositories;
using CarServiceDomain.Services;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CarServiceInfrastructure.Services
{
    public class CarStockService : ICarStockService
    {
        private readonly ICarStockRepository _repository;
        public CarStockService(ICarStockRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<CarStock>> Create(CarStock model)
        {
            model.Id = Guid.NewGuid();
            var carCreted = await _repository.Create(model);
            return carCreted == null
                ? Result.Failure<CarStock>(CarContextExceptionEnum.ErrorCreatingCar.GetErrorMessage())
                : Result.Success(carCreted);
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            bool wasDeleted = await _repository.Delete(id);
            return wasDeleted == true
                ? Result.Success(wasDeleted)
                : Result.Failure<bool>(CarContextExceptionEnum.ErrorDeletingCar.GetErrorMessage());
        }

        public async Task<Result<Tuple<int, IEnumerable<CarStock>>>> GetAll()
        { 
            var cars = await _repository.GetAll();
            return Result.Success(cars);
        }

        public async Task<Result<CarStock>> GetById(Guid id)
        {
            var maybeCar = await _repository.GetById(id);
            if(maybeCar.HasValue)
                return Result.Success(maybeCar.Value);
            return Result.Failure<CarStock>(CarContextExceptionEnum.CarNotFound.GetErrorMessage());
        }

        public async Task<Result<IEnumerable<CarStock>>> GetCarStockByFilter(CarStockByFilterDTO filter)
        {
            var cars = await _repository.GetCarByFilter(filter);
            return cars.Count() > 0
                ? Result.Success(cars)
                : Result.Failure<IEnumerable<CarStock>>(CarContextExceptionEnum.CarNotFoundByFilter.GetErrorMessage());
        }

        public async Task<Result<CarStock>> Update(CarStock model)
        {
            var wasUpdated = await _repository.Update(model);
            return wasUpdated == true
                ? Result.Success(model)
                : Result.Failure<CarStock>(CarContextExceptionEnum.ErrorUpdatingCar.GetErrorMessage());
        }
    }
}
