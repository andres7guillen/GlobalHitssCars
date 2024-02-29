using SparePartsServiceData.Context;
using SparePartsServiceDomain.DTOs;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            model.Id = Guid.NewGuid();
            model.IsInStock = true;
            await _context.SpareParts.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public IQueryable<SparePart> GetSparePartsByFilter(SparePartByFilter filter)
        {
            return _context.SpareParts.Where(sp => filter.BrandCar == null || sp.BrandCar == filter.BrandCar)
                                      .Where(sp => filter.BrandSpare == null || sp.BrandSpare == filter.BrandSpare)
                                      .AsQueryable();
        }

        public async Task<SparePart> UpdatateSpareInStock(SparePart model)
        {
            model.IsInStock = false;
            _context.SpareParts.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
