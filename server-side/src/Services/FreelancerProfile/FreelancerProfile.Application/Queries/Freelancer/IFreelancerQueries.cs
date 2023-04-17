using FluentResults;

namespace FreelancerProfile.Application.Queries
{
    public interface IFreelancerQueries
    {
        Task<Result<FreelancerViewModel>> GetFreelancerFromUserAsync(Guid userId);
    }
}
