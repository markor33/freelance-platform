using ClientProfile.API.Model;

namespace ClientProfile.API.Infrastructure.Repositories
{
    public interface IClientRepository
    {
        Task<Client> GetByUserIdAsync(Guid userId);
        Task<Client> CreateAsync(Client client);
    }
}
