using CSharpFunctionalExtensions;
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
        Task<Tuple<int,IEnumerable<Purchase>>> GetAll(int offset, int limit);
        Task<Maybe<Purchase>> GetById(Guid id);
        Task<bool> Update(Purchase model);
        Task<bool> Delete(Guid id);
    }
}
