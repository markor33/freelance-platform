using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.Repositories;
using FreelancerProfile.Domain.SeedWork;
using FreelancerProfile.Infrastructure.Persistence.LoadingStrategy;

namespace FreelancerProfile.Infrastructure.Persistence.Repositories
{
    public class FreelancerRepository : IFreelancerRepository
    {
        private readonly IAggregateLoadingStrategy _loadingStrategy;
        private readonly FreelancerProfileContext _context;

        public FreelancerRepository(
            ILoadingStrategyFactory loadingStrategyFactory, 
            FreelancerProfileContext context)
        {
            _loadingStrategy = loadingStrategyFactory.CreateLoadingStrategy();
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Freelancer> CreateAsync(Freelancer freelancer)
        {
            return (await _context.AddAsync(freelancer)).Entity;
        }

        public async Task<Freelancer> GetByIdAsync(Guid id)
        {
            return await _loadingStrategy.GetByIdAsync(id);
        }
    }
}
