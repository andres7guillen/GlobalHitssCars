using SparePartsServiceDomain.DTOs;
using SparePartsServiceDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsServiceDomain.Services
{
    public interface ISparePartService
    {
        Task<SparePart> Create(SparePart model);
        Task<IEnumerable<SparePart>> GetSparePartsByFilter(SparePartByFilter filter);
        Task<SparePart> UpdatateSpareInStock(SparePart model);
    }
}
