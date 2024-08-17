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
    public interface ICarRepository
    {
        Task<Car> Create(Car model);
        Task<Tuple<int,IEnumerable<Car>>> GetAll(int offset = 0, int limit = 50);
        Task<Maybe<Car>> GetById(Guid id);
        Task<bool> Update(Car model);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Car>> GetCarByFilter(CarByFilterDTO filter);
    }
}
