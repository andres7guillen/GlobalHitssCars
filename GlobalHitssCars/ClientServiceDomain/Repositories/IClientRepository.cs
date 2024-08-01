using ClientServiceDomain.Entities;
using CSharpFunctionalExtensions;

namespace ClientServiceDomain.Repositories
{
    public interface IClientRepository
    {
        Task<Client> Create(Client model);
        Task<IEnumerable<Client>> GetAll(int offset = 0, int limit = 50);
        Task<Maybe<Client>> GetById(Guid id);
        Task<bool> Update(Client model);
        Task<bool> Delete(Guid id);
    }
}
