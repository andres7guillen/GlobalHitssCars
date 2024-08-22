using CarServiceData.Context;
using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;
using CarServiceDomain.Exceptions;
using CarServiceDomain.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace CarServiceInfrastructure.Repositories
{
    public class CarStockRepository : ICarStockRepository
    {
        private readonly ApplicationCarStockDbContext _context;
        public CarStockRepository(ApplicationCarStockDbContext context)
        {
            _context = context;
        }

        public async Task<CarStock> Create(CarStock model)
        {
            await _context.Cars.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Delete(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return false;
            _context.Cars.Remove(car);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Tuple<int,IEnumerable<CarStock>>> GetAll(int offset = 0,int limit = 50)
        {
            var count = await _context.Cars.AsQueryable().CountAsync();
            var cars = await _context.Cars.AsQueryable()
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
            IEnumerable<CarStock> carCollection = cars;
            return new Tuple<int, IEnumerable<CarStock>>(count, carCollection);
        }

        public async Task<Maybe<CarStock>> GetById(Guid id)
        {
            var car = await _context.Cars
                .FindAsync(id);
            return car == null
                ? Maybe<CarStock>.None
                : Maybe<CarStock>.From(car);
        }

        public async Task<IEnumerable<CarStock>> GetCarByFilter(CarStockByFilterDTO filter)
        {
            return await _context.Cars
                .AsQueryable()
                .Where(c => filter.Colour == null || c.Colour == filter.Colour)
                     .Where(c => filter.Model == null || c.Model == filter.Model)
                     .Where(c => filter.BrandId == null || c.BrandId == filter.BrandId)
                     .ToListAsync();
            
        }

        public async Task<bool> Update(CarStock model)
        {
            _context.Cars.Update(model);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
