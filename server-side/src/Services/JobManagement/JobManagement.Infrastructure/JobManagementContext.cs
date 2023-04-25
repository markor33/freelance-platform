using FreelancerProfile.Domain.SeedWork;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Infrastructure.EntityConfiguration;
using JobManagement.Infrastructure.EntitySeed;
using Microsoft.EntityFrameworkCore;

namespace JobManagement.Infrastructure
{
    public class JobManagementContext : DbContext, IUnitOfWork
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public JobManagementContext(DbContextOptions<JobManagementContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobEntityTypeConfiguration).Assembly);
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
