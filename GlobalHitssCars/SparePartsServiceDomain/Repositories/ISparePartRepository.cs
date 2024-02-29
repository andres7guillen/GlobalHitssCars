using SparePartsServiceDomain.DTOs;
using SparePartsServiceDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsServiceDomain.Repositories
{
    public interface ISparePartRepository
    {
        Task<SparePart> Create(SparePart model);
        IQueryable<SparePart> GetSparePartsByFilter(SparePartByFilter filter);
        Task<SparePart> UpdatateSpareInStock(SparePart model);
    }
}
