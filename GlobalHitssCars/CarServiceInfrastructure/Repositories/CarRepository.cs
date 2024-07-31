using CarServiceData.Context;
using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;
using CarServiceDomain.Exceptions;
using CarServiceDomain.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Car>> GetAll()
        {
            var cars = await _context.Cars.AsQueryable().ToListAsync();
            IEnumerable<Car> carCollection = cars;
            return carCollection;
        }

        public async Task<Maybe<Car>> GetById(Guid id)
        {
            var car = await _context.Cars
                .FindAsync(id);
            return car == null
                ? Maybe<Car>.None
                : Maybe<Car>.From(car);
        }

        public async Task<Maybe<Car>> GetCarByFilter(CarByFilterDTO filter)
        {
            var car = await _context.Cars
                .AsQueryable()
                .Where(c => filter.Colour == null || c.Colour == filter.Colour)
                     .Where(c => filter.Model == null || c.Model == filter.Model)
                     .Where(c => filter.Brand == null || c.Brand == filter.Brand)
                     .FirstOrDefaultAsync();
            return car == null
                ? Maybe<Car>.None
                : Maybe<Car>.From(car);
        }

        public async Task<bool> Update(Car model)
        {
            _context.Cars.Update(model);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
