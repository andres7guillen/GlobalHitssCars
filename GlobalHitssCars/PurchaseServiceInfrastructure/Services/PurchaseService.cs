using Microsoft.EntityFrameworkCore;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Repository;
using PurchaseServiceDomain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseServiceInfrastructure.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _repository;
        public PurchaseService(IPurchaseRepository repository)
        {
                _repository = repository;
        }

        public async Task<Purchase> Create(Purchase model) => await _repository.Create(model);

        public async Task<bool> Delete(Guid id) => await _repository.Delete(id);

        public async Task<IEnumerable<Purchase>> GetAll() => await _repository.GetAll().ToListAsync();

        public async Task<Purchase> GetById(Guid id) => await _repository.GetById(id);

        public async Task<Purchase> Update(Purchase model) => await _repository.Update(model);
    }
}
