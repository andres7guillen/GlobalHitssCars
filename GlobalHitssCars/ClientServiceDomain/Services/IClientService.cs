using ClientServiceDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServiceDomain.Services
{
    public interface IClientService
    {
        Task<Client> Create(Client model);
        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetById(Guid id);
        Task<Client> Update(Client model);
        Task<bool> Delete(Guid id);
    }
}
