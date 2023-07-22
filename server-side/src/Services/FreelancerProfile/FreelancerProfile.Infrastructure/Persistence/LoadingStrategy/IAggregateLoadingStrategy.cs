using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;

namespace FreelancerProfile.Infrastructure.Persistence.LoadingStrategy
{
    public interface IAggregateLoadingStrategy
    {
        Task<Freelancer> GetByIdAsync(Guid id);
    }
}
