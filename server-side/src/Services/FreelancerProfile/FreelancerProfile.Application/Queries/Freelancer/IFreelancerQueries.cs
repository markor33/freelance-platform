using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;

namespace FreelancerProfile.Application.Queries
{
    public interface IFreelancerQueries
    {
        Task<Freelancer> GetFreelancerFromUserAsync(Guid userId);
    }
}
