using ClientServiceData.Context;
using ClientServiceDomain.Entities;
using ClientServiceDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServiceInfrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationClientDbContext _context;
        public ClientRepository(ApplicationClientDbContext context)
        {
            _context = context;
        }

        public async Task<Client> Create(Client model)
        {
            model.Id = Guid.NewGuid();
            _context.Clients.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Delete(Guid id)
        {
            var Client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(Client);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<Client> GetAll()
        {
            return _context.Clients.AsQueryable();
        }

        public async Task<Client> GetById(Guid id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<Client> Update(Client model)
        {
            _context.Clients.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
