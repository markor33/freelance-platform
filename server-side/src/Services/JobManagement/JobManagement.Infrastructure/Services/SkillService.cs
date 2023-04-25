using JobManagement.Application.Services;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobManagement.Infrastructure.Services
{
    public class SkillService : ISkillService
    {
        private readonly JobManagementContext _context;

        public SkillService(JobManagementContext context)
        {
            _context = context;
        }

        public async Task<Skill> GetByIdAsync(Guid id)
        {
            return await _context.Skills.Where(s => s.Id == id).FirstOrDefaultAsync();
        }
    }
}
