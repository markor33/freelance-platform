using ClientProfile.API.Infrastructure.EntityConfiguration;
using ClientProfile.API.Model;
using Microsoft.EntityFrameworkCore;

namespace ClientProfile.API.Infrastructure
{
    public class ClientProfileContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public ClientProfileContext(DbContextOptions<ClientProfileContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ClientEntityConfiguration).Assembly);
        }
    }
}
