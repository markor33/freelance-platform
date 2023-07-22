using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;
using FluentResults;
using FreelancerProfile.Application.Services;
using FreelancerProfile.Domain.Repositories;

namespace FreelancerProfile.Application.Commands
{
    public class ProfileSetupCommandHandler : IRequestHandler<ProfileSetupCommand, Result<Freelancer>>
    {
        private readonly IFreelancerRepository _freelancerRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IProfessionRepository _professionRepository;

        public ProfileSetupCommandHandler(
            IFreelancerRepository freelancerRepository,
            ILanguageRepository languageRepository,
            IProfessionRepository professionRepository)
        {
            _freelancerRepository = freelancerRepository;
            _languageRepository = languageRepository;
            _professionRepository = professionRepository;
        }

        public async Task<Result<Freelancer>> Handle(ProfileSetupCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(request.FreelancerId);

            var profession = await _professionRepository.GetByIdAsync(request.ProfessionId);

            var language = await _languageRepository.GetByIdAsync(request.LanguageId);
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
