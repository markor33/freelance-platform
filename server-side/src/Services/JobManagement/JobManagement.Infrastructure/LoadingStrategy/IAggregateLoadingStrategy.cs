using JobManagement.Domain.AggregatesModel.JobAggregate;

namespace JobManagement.Infrastructure.LoadingStrategy
{
    public interface IAggregateLoadingStrategy
    {
        Task<Job> GetByIdAsync(Guid id);
    }
}
