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

        public async Task<Freelancer> GetByIdAsync(Guid id)
        {
            return await _context.Freelancers
                .Include(f => f.Skills)
                .Include(f => f.Certifications)
                .Where(f => f.Id == id).FirstOrDefaultAsync(); 
        }

        public async Task<Freelancer> GetByUserIdAsync(Guid userId)
        {
            return await _context.Freelancers
                .Include(f => f.Skills)
                .Include(f => f.Certifications)
                .Where(f => f.UserId== userId).FirstOrDefaultAsync();
        }
    }
}
