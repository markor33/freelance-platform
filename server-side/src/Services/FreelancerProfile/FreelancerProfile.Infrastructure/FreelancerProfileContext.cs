using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entites;
using FreelancerProfile.Domain.SeedWork;
using FreelancerProfile.Infrastructure.EntityConfiguration;
using FreelancerProfile.Infrastructure.EntitySeed;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure
{
    public class FreelancerProfileContext : DbContext, IUnitOfWork
    {
        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public FreelancerProfileContext(DbContextOptions<FreelancerProfileContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FreelancerEntityTypeConfiguration).Assembly);
            modelBuilder.Entity<Language>().SeedLanguage();
            modelBuilder.SeedProfession();
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
