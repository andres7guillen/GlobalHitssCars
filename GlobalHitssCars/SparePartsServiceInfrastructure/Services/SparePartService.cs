using Microsoft.EntityFrameworkCore;
using SparePartsServiceDomain.DTOs;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Repositories;
using SparePartsServiceDomain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsServiceInfrastructure.Services
{
    public class SparePartService : ISparePartService
    {
        private readonly ISparePartRepository _repository;

        public SparePartService(ISparePartRepository repository)
        {
            _repository = repository;
        }

        public async Task<SparePart> Create(SparePart model) => await _repository.Create(model);

        public async Task<IEnumerable<SparePart>> GetSparePartsByFilter(SparePartByFilter filter) => await _repository.GetSparePartsByFilter(filter).ToListAsync();

        public async Task<SparePart> UpdatateSpareInStock(SparePart model) => await _repository.UpdatateSpareInStock(model);
    }
}
