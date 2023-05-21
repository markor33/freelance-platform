using FreelancerProfile.Domain.SeedWork;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using Microsoft.EntityFrameworkCore;

namespace JobManagement.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly JobManagementContext _context;

        public JobRepository(JobManagementContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Job> GetByIdAsync(Guid id)
        {
            return await _context.Jobs.Where(j => j.Id== id)
                .Include(j => j.Proposals)
                .Include(j => j.Questions)
                .Include(j => j.Contracts)
                .FirstOrDefaultAsync();
        }

        public async Task<Job> CreateAsync(Job job)
        {
            return (await _context.Jobs.AddAsync(job)).Entity;
        }

    }
}
