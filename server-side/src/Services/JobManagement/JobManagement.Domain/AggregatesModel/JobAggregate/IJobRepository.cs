using FreelancerProfile.Domain.SeedWork;

namespace JobManagement.Domain.AggregatesModel.JobAggregate
{
    public interface IJobRepository : IRepository<Job>
    {
        Task<Job> GetByIdAsync(Guid id);
        Task<Job> CreateAsync(Job job);
    }
}
