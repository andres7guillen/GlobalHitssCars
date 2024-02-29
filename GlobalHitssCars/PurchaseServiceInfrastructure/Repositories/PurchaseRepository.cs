using PurchaseServiceData.Context;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseServiceInfrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationPurchaseDbContext _context;
        public PurchaseRepository(ApplicationPurchaseDbContext context)
        {
                _context = context;
        }

        public async Task<Purchase> Create(Purchase model)
        {
            model.Id = Guid.NewGuid();
            _context.Purchases.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Delete(Guid id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            _context.Purchases.Remove(purchase);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<Purchase> GetAll()
        {
            return _context.Purchases.AsQueryable();
        }

        public async Task<Purchase> GetById(Guid id)
        {
            return await _context.Purchases.FindAsync(id);
        }

        public async Task<Purchase> Update(Purchase model)
        {
            _context.Purchases.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
