using FluentResults;

namespace FreelancerProfile.Application.Queries
{
    public class FreelancerQueries : IFreelancerQueries
    {
        private readonly IFreelancerReadModelRepository _repository;

        public FreelancerQueries(IFreelancerReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<FreelancerViewModel>> GetByIdAsync(Guid id)
        {
            var freelancer = await _repository.GetByIdAsync(id);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");
            return Result.Ok(freelancer);
        }

        public async Task<Result<FreelancerViewModel>> GetByUserIdAsync(Guid userId)
        {
            var freelancer = await _repository.GetByUserIdAsync(userId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");
            return Result.Ok(freelancer);
        }
    }
}
