using FreelancerProfile.Application.Services;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly FreelancerProfileContext _context;

        public LanguageService(FreelancerProfileContext context)
        {
            _context = context;
        }

        public async Task<Language> GetByIdAsync(int id)
        {
            return await _context.Languages.Where(l => l.Id == id).FirstOrDefaultAsync();
        }
    }
}
