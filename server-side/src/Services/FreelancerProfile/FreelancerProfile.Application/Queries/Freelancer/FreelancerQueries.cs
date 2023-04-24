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

        public async Task<Result<FreelancerViewModel>> GetFreelancerFromUserAsync(Guid userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }
    }
}
