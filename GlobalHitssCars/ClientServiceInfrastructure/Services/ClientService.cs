using ClientServiceDomain.Entities;
using ClientServiceDomain.Repositories;
using ClientServiceDomain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServiceInfrastructure.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }
        public async Task<Client> Create(Client model) => await _repository.Create(model);

        public async Task<bool> Delete(Guid id) => await _repository.Delete(id);

        public async Task<Tuple<int, IEnumerable<Client>>> GetAll() => await _repository.GetAll();

        public async Task<Client> GetById(Guid id) 
        {
            var clientMaybe = await _repository.GetById(id);
            return clientMaybe.Value;
        } 

        public async Task<bool> Update(Client model) => await _repository.Update(model);
    }
}
