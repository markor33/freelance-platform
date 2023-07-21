using JobManagement.Domain.AggregatesModel.JobAggregate;
using Microsoft.EntityFrameworkCore;

namespace JobManagement.Infrastructure.LoadingStrategy
{
    public class StandardLoadingStrategy : IAggregateLoadingStrategy
    {
        private readonly JobManagementContext _context;

        public StandardLoadingStrategy(JobManagementContext context)
        {
            _context = context;
        }

        public async Task<Job> GetByIdAsync(Guid id)
        {
            return await _context.Jobs.Where(j => j.Id == id)
                .Include(s => s.Skills)
                .Include(j => j.Proposals)
                .Include(j => j.Questions)
                .Include(j => j.Contracts)
                .FirstOrDefaultAsync();
        }
    }
}
