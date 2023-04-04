using AutoMapper;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;

namespace FreelancerProfile.Application.Queries
{
    public class FreelancerQueries : IFreelancerQueries
    {
        private readonly IFreelancerRepository _repository;
        private readonly IMapper _mapper;

        public FreelancerQueries(IFreelancerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FreelancerViewModel> GetFreelancerFromUserAsync(Guid userId)
        {
            var freelancer = await _repository.GetByUserIdAsync(userId);
            if (freelancer is null)
                return null;
            return _mapper.Map<FreelancerViewModel>(freelancer);
        }

    }
}
