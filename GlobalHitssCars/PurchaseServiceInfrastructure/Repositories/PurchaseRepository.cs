using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
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
            _context.Purchases.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Delete(Guid id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
                return false;
            _context.Purchases.Remove(purchase);
            return await _context.SaveChangesAsync() > 0
                ? true
                : false;
        }

        public async Task<Tuple<int,IEnumerable<Purchase>>> GetAll(int offset = 0, int limit = 10)
        {
            var total = await _context.Purchases.AsQueryable().CountAsync();
            var list = await _context.Purchases
                .Skip(offset)
                .Take(limit)
                .AsQueryable().ToListAsync();
            IEnumerable<Purchase> purchases = list;
            return new Tuple<int, IEnumerable<Purchase>>(total, purchases);
        }

        public async Task<Maybe<Purchase>> GetById(Guid id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            return purchase == null
            ? Maybe<Purchase>.None
            : Maybe<Purchase>.From(purchase);
        }

        public async Task<bool> Update(Purchase model)
        {
            _context.Purchases.Update(model);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
