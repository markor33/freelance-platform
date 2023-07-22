using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure.Persistence.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly FreelancerProfileContext _context;

        public LanguageRepository(FreelancerProfileContext context)
        {
            _context = context;
        }

        public async Task<Language> GetByIdAsync(int id)
        {
            return await _context.Languages.Where(l => l.Id == id).FirstOrDefaultAsync();
        }
    }
}
