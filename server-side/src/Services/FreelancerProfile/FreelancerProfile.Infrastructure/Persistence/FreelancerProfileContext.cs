using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.SeedWork;
using FreelancerProfile.Infrastructure.Persistence.EntityConfiguration;
using FreelancerProfile.Infrastructure.Persistence.EntitySeed;
using FreelancerProfile.Infrastructure.Persistence.EventStore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace FreelancerProfile.Infrastructure.Persistence
{
    public class FreelancerProfileContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;
        private IDbContextTransaction _currentTransaction;

        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<DomainEventLog> DomainEventLogs { get; set; }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        public FreelancerProfileContext(
            DbContextOptions<FreelancerProfileContext> options,
            IEventStore eventStore,
            IMediator mediator) : base(options)
        {
            _eventStore = eventStore;
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
                await _eventStore.SaveEventsAsync(this);

                await base.SaveChangesAsync(cancellationToken);

                await _mediator.DispatchDomainEventsAsync(this);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null) return;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                await _currentTransaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _currentTransaction?.RollbackAsync();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

    }
}
