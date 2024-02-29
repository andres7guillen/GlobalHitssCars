using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;
using CarServiceDomain.Repositories;
using CarServiceDomain.Services;
using Microsoft.EntityFrameworkCore;

namespace CarServiceInfrastructure.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _repository;
        public CarService(ICarRepository repository)
        {
            _repository = repository;
        }

        public async Task<Car> Create(Car model) => await _repository.Create(model);

        public async Task<bool> Delete(Guid id) => await _repository.Delete(id);

        public async Task<IEnumerable<Car>> GetAll() => await _repository.GetAll().ToListAsync();

        public async Task<Car> GetById(Guid id) => await _repository.GetById(id);

        public async Task<Car> GetCarByFilter(CarByFilterDTO filter) => await _repository.GetCarByFilter(filter).FirstOrDefaultAsync();

        public async Task<Car> Update(Car model) => await _repository.Update(model);
    }
}
