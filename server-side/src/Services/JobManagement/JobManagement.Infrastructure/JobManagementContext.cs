using JobManagement.Domain.SeedWork;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Infrastructure.EntityConfiguration;
using JobManagement.Infrastructure.EntitySeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using JobManagement.Infrastructure.EventStore;

namespace JobManagement.Infrastructure
{
    public class JobManagementContext : DbContext, IUnitOfWork
    {
        private readonly IEventStore _eventStore;
        private IDbContextTransaction _currentTransaction;

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<DomainEventLog> DomainEventLogs { get; set; }
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        public JobManagementContext(
            DbContextOptions<JobManagementContext> options, 
            IEventStore eventStore) : base(options)
        {
            _eventStore = eventStore;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobEntityTypeConfiguration).Assembly);
            modelBuilder.SeedProfession();
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _eventStore.SaveEventsAsync(this);

                await base.SaveChangesAsync(cancellationToken);

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
