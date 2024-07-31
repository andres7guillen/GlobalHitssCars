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
    public class CarService : ICarService
    {
        private readonly ICarRepository _repository;
        public CarService(ICarRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Car>> Create(Car model)
        {
            var carCreted = await _repository.Create(model);
            return carCreted == null
                ? Result.Failure<Car>(CarContextExceptionEnum.ErrorCreatingCar.GetErrorMessage())
                : Result.Success(carCreted);
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            bool wasDeleted = await _repository.Delete(id);
            return wasDeleted == true
                ? Result.Success(wasDeleted)
                : Result.Failure<bool>(CarContextExceptionEnum.ErrorDeleteingCar.GetErrorMessage());
        }

        public async Task<Result<IEnumerable<Car>>> GetAll()
        { 
            var cars = await _repository.GetAll();
            return Result.Success(cars);
        }

        public async Task<Result<Car>> GetById(Guid id)
        {
            var maybeCar = await _repository.GetById(id);
            if(maybeCar.HasValue)
                return Result.Success(maybeCar.Value);
            return Result.Failure<Car>(CarContextExceptionEnum.CarNotFound.GetErrorMessage());
        }

        public async Task<Result<Car>> GetCarByFilter(CarByFilterDTO filter)
        {
            var carMaybe = await _repository.GetCarByFilter(filter);
            return carMaybe.HasValue == true
                ? Result.Success(carMaybe.Value)
                : Result.Failure<Car>(CarContextExceptionEnum.CarNotFoundByFilter.GetErrorMessage());
        }

        public async Task<Result<Car>> Update(Car model)
        {
            var wasUpdated = await _repository.Update(model);
            return wasUpdated == true
                ? Result.Success(model)
                : Result.Failure<Car>(CarContextExceptionEnum.ErrorUpdatingCar.GetErrorMessage());
        }
    }
}
