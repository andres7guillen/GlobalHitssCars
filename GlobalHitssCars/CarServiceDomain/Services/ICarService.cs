using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;
using CSharpFunctionalExtensions;



namespace CarServiceDomain.Services
{
    public interface ICarService
    {
        Task<Result<Car>> Create(Car model);
        Task<Result<IEnumerable<Car>>> GetAll();
        Task<Result<Car>> GetById(Guid id);
        Task<Result<Car>> Update(Car model);
        Task<Result<bool>> Delete(Guid id);
        Task<Result<IEnumerable<Car>>> GetCarByFilter(CarByFilterDTO filter);
    }
}
