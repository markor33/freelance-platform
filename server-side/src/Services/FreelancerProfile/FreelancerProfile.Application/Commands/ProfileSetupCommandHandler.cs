using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;
using FluentResults;
using FreelancerProfile.Application.Services;

namespace FreelancerProfile.Application.Commands
{
    public class ProfileSetupCommandHandler : IRequestHandler<ProfileSetupCommand, Result<Freelancer>>
    {
        private readonly IFreelancerRepository _freelancerRepository;
        private readonly ILanguageService _languageService;
        private readonly IProfessionService _professionService;

        public ProfileSetupCommandHandler(
            IFreelancerRepository freelancerRepository, 
            ILanguageService languageService,
            IProfessionService professionService)
        {
            _freelancerRepository = freelancerRepository;
            _languageService = languageService;
            _professionService= professionService;
        }

        public async Task<Result<Freelancer>> Handle(ProfileSetupCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(request.FreelancerId);

            var profession = await _professionService.GetByIdAsync(request.ProfessionId);

            var language = await _languageService.GetByIdAsync(request.LanguageId);
            var languageKnowledge = new LanguageKnowledge(language, request.LanguageProficiencyLevel);

            freelancer.SetupProfile(request.IsProfilePublic, request.ProfileSummary, request.HourlyRate,
                request.Availability, request.ExperienceLevel, profession, languageKnowledge);

            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!result)
                return Result.Fail("Freelancer profile setup failed");
            return Result.Ok(freelancer);
        }

    }
}
