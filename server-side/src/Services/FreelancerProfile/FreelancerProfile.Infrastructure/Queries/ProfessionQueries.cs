using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entites;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure.Queries
{
    public class ProfessionQueries : IProfessionQueries
    {
        private readonly FreelancerProfileContext _context;

        public ProfessionQueries(FreelancerProfileContext context)
        {
            _context = context;
        }

        public async Task<Profession> GetByIdAsync(Guid id)
            => await _context.Professions.Where(p => p.Id == id).FirstOrDefaultAsync();
    }
}
