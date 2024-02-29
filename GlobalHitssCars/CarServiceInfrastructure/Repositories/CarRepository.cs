using CarServiceData.Context;
using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;
using CarServiceDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceInfrastructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationCarDbContext _context;
        public CarRepository(ApplicationCarDbContext context)
        {
            _context = context;
        }

        public async Task<Car> Create(Car model)
        {
            model.Id = Guid.NewGuid();
            await _context.Cars.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Delete(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<Car> GetAll()
        {
            return _context.Cars.AsQueryable();
        }

        public async Task<Car> GetById(Guid id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public IQueryable<Car> GetCarByFilter(CarByFilterDTO filter)
        {
            return _context.Cars.Where(c => filter.Colour == null || c.Colour == filter.Colour)
                         .Where(c => filter.Model == null || c.Model == filter.Model)
                         .Where(c => filter.Brand == null || c.Brand == filter.Brand)
                         .AsQueryable();
        }

        public async Task<Car> Update(Car model)
        {
            _context.Cars.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
