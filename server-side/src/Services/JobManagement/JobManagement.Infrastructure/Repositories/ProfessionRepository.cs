using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobManagement.Infrastructure.Repositories
{
    public class ProfessionRepository : IProfessionRepository
    {
        private readonly JobManagementContext _context;

        public ProfessionRepository(JobManagementContext context)
        {
            _context = context;
        }

        public async Task<Profession> GetByIdAsync(Guid id)
        {
            return await _context.Professions.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
