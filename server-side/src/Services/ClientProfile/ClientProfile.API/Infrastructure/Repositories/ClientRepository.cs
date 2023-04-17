using ClientProfile.API.Model;
using Microsoft.EntityFrameworkCore;

namespace ClientProfile.API.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientProfileContext _context;

        public ClientRepository(ClientProfileContext context)
        {
            _context = context;
        }

        public async Task<Client> CreateAsync(Client client)
        { 
            client = (await _context.Clients.AddAsync(client)).Entity;
            if (client is null)
                return null;
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> GetByUserIdAsync(Guid userId)
            => await _context.Clients.Where(c => c.UserId == userId).FirstOrDefaultAsync();

    }
}
