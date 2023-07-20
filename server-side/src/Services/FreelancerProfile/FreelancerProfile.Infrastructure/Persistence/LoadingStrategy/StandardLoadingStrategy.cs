using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure.Persistence.LoadingStrategy
{
    public class StandardLoadingStrategy : IAggregateLoadingStrategy
    {
        private readonly FreelancerProfileContext _context;

        public StandardLoadingStrategy(FreelancerProfileContext context)
        {
            _context = context;
        }

        public async Task<Freelancer> GetByIdAsync(Guid id)
        {
            return await _context.Freelancers
                .Include(f => f.LanguageKnowledges)
                .Include(f => f.Skills)
                .Include(f => f.Educations)
                .Include(f => f.Certifications)
                .Include(f => f.Employments)
                .Where(f => f.Id == id).FirstOrDefaultAsync();
        }
    }
}
