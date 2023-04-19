using FreelancerProfile.Domain.SeedWork;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace JobManagement.Infrastructure
{
    public class JobManagementContext : DbContext, IUnitOfWork
    {
        public DbSet<Job> Jobs { get; set; }

        public JobManagementContext(DbContextOptions<JobManagementContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobEntityConfiguration).Assembly);
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
