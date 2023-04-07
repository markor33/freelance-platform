using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entites;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure.Queries
{
    public class LanguageQueries : ILanguageQueries
    {
        private readonly FreelancerProfileContext _context;

        public LanguageQueries(FreelancerProfileContext context)
        {
            _context = context;
        }

        public async Task<Language> GetByIdAsync(int id)
            => await _context.Languages.Where(l => l.Id == id).FirstOrDefaultAsync();
    }
}
