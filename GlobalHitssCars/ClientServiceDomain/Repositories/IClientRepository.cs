using ClientServiceDomain.Entities;

namespace ClientServiceDomain.Repositories
{
    public interface IClientRepository
    {
        Task<Client> Create(Client model);
        IQueryable<Client> GetAll();
        Task<Client> GetById(Guid id);
        Task<Client> Update(Client model);
        Task<bool> Delete(Guid id);
    }
}
