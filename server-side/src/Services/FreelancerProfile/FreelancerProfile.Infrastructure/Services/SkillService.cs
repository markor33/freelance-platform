using FreelancerProfile.Application.Services;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure.Services
{
    public class SkillService : ISkillService
    {
        private readonly FreelancerProfileContext _context;

        public SkillService(FreelancerProfileContext context)
        {
            _context = context;
        }

        public async Task<Skill> GetByIdAsync(Guid id)
        {
            return await _context.Skills.Where(s => s.Id == id).FirstOrDefaultAsync();
        }
    }
}
