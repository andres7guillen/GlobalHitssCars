using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SparePartsServiceData.Context;
using SparePartsServiceDomain.DTOs;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Exceptions;
using SparePartsServiceDomain.Repositories;

namespace SparePartsServiceInfrastructure.Repositories
{
    public class SparePartRepository : ISparePartRepository
    {
        private readonly ApplicationSparePartDbContext _context;

        public SparePartRepository(ApplicationSparePartDbContext context)
        {
            _context = context;
        }

        public async Task<SparePart> Create(SparePart model)
        {
            model.IsInStock = true;
            await _context.SpareParts.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteSparePart(Guid id)
        {
            var spare = await _context.SpareParts.FindAsync(id);
            if (spare == null)
                return false;
            _context.SpareParts.Remove(spare);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<SparePart>> GetAllSpareParts(int offset = 0, int limit = 10)
        {
            var spares = await _context.SpareParts
                .Skip(offset)
                .Take(limit)
                .AsQueryable()
                .ToListAsync();
            IEnumerable<SparePart> list = spares;
            return list;
        }

        public async Task<Maybe<SparePart>> GetSparePartById(Guid id)
        {
            var spare = await _context.SpareParts.FindAsync(id);
            return spare == null
                ? Maybe<SparePart>.None
                : Maybe<SparePart>.From(spare);
        }

        public async Task<IEnumerable<SparePart>> GetSparePartsByFilter(GetSparePartByFilterDTO filter)
        {
            var spares = await _context.SpareParts.Where(sp => filter.BrandCar == null || sp.BrandCar == filter.BrandCar)
                                      .Where(sp => filter.BrandSpare == null || sp.BrandSpare == filter.BrandSpare)
                                      .ToListAsync();
            IEnumerable<SparePart> spareList = spares;
            return spareList;
        }

        public async Task<Result<int>> GetStockBySpareId(Guid id)
        {
            var spare = await GetSparePartById(id);
            return spare.HasValue
                ? Result.Success(spare.Value.Stock)
                : Result.Failure<int>(SparePartContextExceptionEnum.SparePartNotFound.GetErrorMessage());
        }

        public async Task<bool> LessStock(Guid id, int stockQuantity)
        {
            var spare = await _context.SpareParts.FindAsync(id);
            if (spare != null)
            {
                spare.Stock -= stockQuantity;
                return await _context.SaveChangesAsync() > 0;
            }  
            return false;
        }

        public async Task<bool> AddStock(Guid id, int quantity)
        {
            var spare = await _context.SpareParts.FindAsync(id);
            if (spare != null)
            {
                spare.Stock += quantity;
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> UpdatateSpare(SparePart model)
        {
            _context.SpareParts.Update(model);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
