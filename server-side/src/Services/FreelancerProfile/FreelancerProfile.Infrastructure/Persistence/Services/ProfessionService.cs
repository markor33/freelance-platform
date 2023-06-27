using FreelancerProfile.Application.Services;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure.Persistence.Services
{
    public class ProfessionService : IProfessionService
    {
        private readonly FreelancerProfileContext _context;

        public ProfessionService(FreelancerProfileContext context)
        {
            _context = context;
        }

        public async Task<Profession> GetByIdAsync(Guid id)
        {
            return await _context.Professions.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
