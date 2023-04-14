using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entites;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure.Queries
{
    public class SkillQueries : ISkillQueries
    {
        private readonly FreelancerProfileContext _context;

        public SkillQueries(FreelancerProfileContext context)
        {
            _context = context;
        }

        public async Task<Skill> GetByIdAsync(Guid id)
            => await _context.Skills.Where(s => s.Id == id).FirstOrDefaultAsync();

        public async Task<List<Skill>> GetByProfession(Guid professionId)
            => await _context.Skills.Where(s => s.ProfessionId == professionId).ToListAsync();

    }
}
