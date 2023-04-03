using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure.Repositories
{
    public class FreelancerRepository : IFreelancerRepository
    {
        private readonly FreelancerProfileContext _context;

        public FreelancerRepository(FreelancerProfileContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Freelancer> CreateAsync(Freelancer freelancer)
        {
            return (await _context.AddAsync(freelancer)).Entity;
        }

        public async Task<Freelancer> GetByUserId(Guid userId)
        {
            return await _context.Freelancers.Where(f => f.UserId== userId).FirstOrDefaultAsync();
        }
    }
}
