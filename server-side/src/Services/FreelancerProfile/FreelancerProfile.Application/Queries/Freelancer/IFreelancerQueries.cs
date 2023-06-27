using FluentResults;

namespace FreelancerProfile.Application.Queries
{
    public interface IFreelancerQueries
    {
        Task<Result<FreelancerViewModel>> GetByIdAsync(Guid id);
        Task<Result<FreelancerViewModel>> GetByUserIdAsync(Guid userId);
    }
}
