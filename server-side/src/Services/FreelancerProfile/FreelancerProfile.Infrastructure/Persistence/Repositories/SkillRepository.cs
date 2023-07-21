using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly FreelancerProfileContext _context;

        public SkillRepository(FreelancerProfileContext context)
        {
            _context = context;
        }

        public async Task<Skill> GetByIdAsync(Guid id)
        {
            return await _context.Skills.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Skill>> GetByIdsAsync(List<Guid> ids)
        {
            return await _context.Skills.Where(s => ids.Any(x => x == s.Id)).ToListAsync();
        }
    }
}
