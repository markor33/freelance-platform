using AutoMapper;
using FreelancerProfile.Application.Queries;
using Microsoft.EntityFrameworkCore;

namespace FreelancerProfile.Infrastructure.Queries
{
    public class FreelancerQueries : IFreelancerQueries
    {
        private readonly FreelancerProfileContext _context;
        private readonly IMapper _mapper;

        public FreelancerQueries(FreelancerProfileContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FreelancerViewModel> GetFreelancerFromUserAsync(Guid userId)
        {
            var freelancer = await _context.Freelancers
                                    .Include(f => f.LanguageKnowledges)
                                        .ThenInclude(lk => lk.Language)
                                    .Include(f => f.Profession)
                                    .Include(f => f.Educations)
                                    .Include(f => f.Certifications)
                                    .Include(f => f.Employments)
                                    .Include(f => f.Skills)
                                    .Where(f => f.UserId == userId)
                                    .FirstOrDefaultAsync();
            if (freelancer is null)
                return null;
            return _mapper.Map<FreelancerViewModel>(freelancer);
        }

    }
}
