using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure.Queries
{
    public class FreelancerQueries : IFreelancerQueries
    {
        private readonly FreelancerProfileContext _context;

        public FreelancerQueries(FreelancerProfileContext context)
        {
            _context = context;
        }

        public async Task<Freelancer> GetFreelancerFromUserAsync(Guid userId)
             => await _context.Freelancers
                .Include(f => f.LanguageKnowledges)
                    .ThenInclude(lk => lk.Language)
                .Include(f => f.Profession)
                .Include(f => f.Educations)
                .Include(f => f.Certifications)
                .Where(f => f.UserId == userId)
                .FirstOrDefaultAsync();
    }
}
