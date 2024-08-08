using PurchaseServiceDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseServiceDomain.Services
{
    public interface IPurchaseService
    {
        Task<Purchase> Create(Purchase model);
        Task<IEnumerable<Purchase>> GetAll(int offset, int limit);
        Task<Purchase> GetById(Guid id);
        Task<bool> Update(Purchase model);
        Task<bool> Delete(Guid id);
    }
}
