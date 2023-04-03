using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.SeedWork;
using FreelancerProfile.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure
{
    public class FreelancerProfileContext : DbContext, IUnitOfWork
    {
        public DbSet<Freelancer> Freelancers { get; set; }

        public FreelancerProfileContext(DbContextOptions<FreelancerProfileContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FreelancerEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await base.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
