using JobManagement.Application.Services;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobManagement.Infrastructure.Services
{
    public class ProfessionService : IProfessionService
    {
        private readonly JobManagementContext _context;

        public ProfessionService(JobManagementContext context)
        {
            _context = context;
        }

        public async Task<Profession> GetByIdAsync(Guid id)
        {
            return await _context.Professions.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
