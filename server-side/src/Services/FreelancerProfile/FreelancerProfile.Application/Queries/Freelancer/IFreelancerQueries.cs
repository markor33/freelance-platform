using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;

namespace FreelancerProfile.Application.Queries
{
    public interface IFreelancerQueries
    {
        Task<FreelancerViewModel> GetFreelancerFromUserAsync(Guid userId);
    }
}
