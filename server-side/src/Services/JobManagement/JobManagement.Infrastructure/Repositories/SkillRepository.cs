using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobManagement.Infrastructure.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly JobManagementContext _context;

        public SkillRepository(JobManagementContext context)
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
