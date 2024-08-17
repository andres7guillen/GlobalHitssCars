using ClientServiceData.Context;
using ClientServiceDomain.Entities;
using ClientServiceDomain.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
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
            _context.Clients.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Delete(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                return false;
            _context.Clients.Remove(client);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Tuple<int,IEnumerable<Client>>> GetAll(int offset = 0, int limit = 50)
        {
            var total = await _context.Clients.AsQueryable().CountAsync();
            var list = await _context.Clients.AsQueryable()
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
            IEnumerable<Client> clientsCollection = list;
            return new Tuple<int, IEnumerable<Client>>(total, clientsCollection);
        }

        public async Task<Maybe<Client>> GetById(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            return client == null
                ? Maybe<Client>.None
                : Maybe<Client>.From(client);
        }

        public async Task<bool> Update(Client model)
        {
            _context.Clients.Update(model);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
