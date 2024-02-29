using CarServiceDomain.DTOs;
using CarServiceDomain.Entities;
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
        IQueryable<Car> GetAll();
        Task<Car> GetById(Guid id);
        Task<Car> Update(Car model);
        Task<bool> Delete(Guid id);
        IQueryable<Car> GetCarByFilter(CarByFilterDTO filter);
    }
}
