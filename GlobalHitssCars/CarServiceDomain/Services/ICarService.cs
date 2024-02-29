using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;



namespace CarServiceDomain.Services
{
    public interface ICarService
    {
        Task<Car> Create(Car model);
        Task<IEnumerable<Car>> GetAll();
        Task<Car> GetById(Guid id);
        Task<Car> Update(Car model);
        Task<bool> Delete(Guid id);
        Task<Car> GetCarByFilter(CarByFilterDTO filter);
    }
}
