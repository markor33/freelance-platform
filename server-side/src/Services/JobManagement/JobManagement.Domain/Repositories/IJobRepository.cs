using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.SeedWork;

namespace JobManagement.Domain.Repositories
{
    public interface IJobRepository : IRepository<Job>
    {
        Task<Job> GetByIdAsync(Guid id);
        Task<Job> CreateAsync(Job job);
        Task<bool> IsJobOwner(Guid jobId, Guid clientId);
        Task<bool> IsProposalOwner(Guid proposalId, Guid freelancerId);
    }
}
