using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure.Persistence.Repositories
{
    public class ProfessionRepository : IProfessionRepository
    {
        private readonly FreelancerProfileContext _context;

        public ProfessionRepository(FreelancerProfileContext context)
        {
            _context = context;
        }

        public async Task<Profession> GetByIdAsync(Guid id)
        {
            return await _context.Professions.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
