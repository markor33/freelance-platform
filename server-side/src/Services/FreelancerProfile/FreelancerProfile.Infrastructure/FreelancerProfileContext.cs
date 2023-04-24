using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.SeedWork;
using FreelancerProfile.Infrastructure.EntityConfiguration;
using FreelancerProfile.Infrastructure.EntitySeed;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure
{
    public class FreelancerProfileContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public FreelancerProfileContext(
            DbContextOptions<FreelancerProfileContext> options,
            IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

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

                await _mediator.DispatchDomainEventsAsync(this);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
