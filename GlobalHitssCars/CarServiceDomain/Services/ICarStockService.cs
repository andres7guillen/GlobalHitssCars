using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;
using CSharpFunctionalExtensions;



namespace CarServiceDomain.Services
{
    public interface ICarStockService
    {
        Task<Result<CarStock>> Create(CarStock model);
        Task<Result<Tuple<int, IEnumerable<CarStock>>>> GetAll();
        Task<Result<CarStock>> GetById(Guid id);
        Task<Result<CarStock>> Update(CarStock model);
        Task<Result<bool>> Delete(Guid id);
        Task<Result<IEnumerable<CarStock>>> GetCarStockByFilter(CarStockByFilterDTO filter);
    }
}
