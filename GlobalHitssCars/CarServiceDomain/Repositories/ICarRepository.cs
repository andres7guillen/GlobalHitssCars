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
        Task<IEnumerable<Car>> GetAll();
        Task<Maybe<Car>> GetById(Guid id);
        Task<bool> Update(Car model);
        Task<bool> Delete(Guid id);
        Task<Maybe<Car>> GetCarByFilter(CarByFilterDTO filter);
    }
}
