using CSharpFunctionalExtensions;
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
        Task<IEnumerable<SparePart>> GetSparePartsByFilter(GetSparePartByFilterDTO filter);
        Task<Tuple<int,IEnumerable<SparePart>>> GetAllSpareParts(int offset, int limit);
        Task<bool> UpdatateSpare(SparePart model);
        Task<Maybe<SparePart>> GetSparePartById(Guid id);
        Task<Result<int>> GetStockBySpareId(Guid id);
        Task<bool> DeleteSparePart(Guid id);
    }
}
