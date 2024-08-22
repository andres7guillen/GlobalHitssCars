using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceDomain.Repositories
{
    public interface ICarStockRepository
    {
        Task<CarStock> Create(CarStock model);
        Task<Tuple<int,IEnumerable<CarStock>>> GetAll(int offset = 0, int limit = 50);
        Task<Maybe<CarStock>> GetById(Guid id);
        Task<bool> Update(CarStock model);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<CarStock>> GetCarByFilter(CarStockByFilterDTO filter);
    }
}
