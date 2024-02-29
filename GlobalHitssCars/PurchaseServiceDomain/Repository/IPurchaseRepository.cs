using PurchaseServiceDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseServiceDomain.Repository
{
    public interface IPurchaseRepository
    {
        Task<Purchase> Create(Purchase model);
        IQueryable<Purchase> GetAll();
        Task<Purchase> GetById(Guid id);
        Task<Purchase> Update(Purchase model);
        Task<bool> Delete(Guid id);
    }
}
