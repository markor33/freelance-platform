using JobManagement.Domain.SeedWork;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Infrastructure.LoadingStrategy;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using JobManagement.Domain.Repositories;

namespace JobManagement.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IAggregateLoadingStrategy _loadingStrategy;
        private readonly JobManagementContext _context;

        public JobRepository(
            ILoadingStrategyFactory loadingStrategyFactory,
            JobManagementContext context)
        {
            _loadingStrategy = loadingStrategyFactory.CreateLoadingStrategy();
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Job> GetByIdAsync(Guid id)
        {
            var job = await _loadingStrategy.GetByIdAsync(id);
            return job;
        }

        public async Task<Job> CreateAsync(Job job)
        {
            return (await _context.Jobs.AddAsync(job)).Entity;
        }

        public async Task<bool> IsJobOwner(Guid jobId, Guid clientId)
        {
            var job = await _context.Jobs
                .Where(j => j.Id == jobId && j.ClientId == clientId)
                .Select(j => j.Id)
                .FirstOrDefaultAsync();

            return job != default;
        }

        public async Task<bool> IsProposalOwner(Guid proposalId, Guid freelancerId)
        {
            var proposal = await _context.Proposals
                .Where(p => p.Id == proposalId && p.FreelancerId == freelancerId)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();

            return proposal != default;
        }
    }
}
