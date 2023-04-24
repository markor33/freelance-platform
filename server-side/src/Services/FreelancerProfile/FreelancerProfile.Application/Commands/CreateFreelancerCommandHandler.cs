using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;
using FluentResults;
using FreelancerProfile.Application.Services;

namespace FreelancerProfile.Application.Commands
{
    public class CreateFreelancerCommandHandler : IRequestHandler<CreateFreelancerCommand, Result<Freelancer>>
    {
        private readonly IFreelancerRepository _freelancerRepository;
        private readonly ILanguageService _languageService;
        private readonly IProfessionService _professionService;

        public CreateFreelancerCommandHandler(
            IFreelancerRepository freelancerRepository, 
            ILanguageService languageService,
            IProfessionService professionService)
        {
            _freelancerRepository = freelancerRepository;
            _languageService = languageService;
            _professionService= professionService;
        }

        public async Task<Result<Freelancer>> Handle(CreateFreelancerCommand request, CancellationToken cancellationToken)
        {
            if (await _freelancerRepository.GetByUserIdAsync(request.UserId) is not null)
                return Result.Fail("Freelancer already created");

            var profession = await _professionService.GetByIdAsync(request.ProfessionId);
            if (profession is null)
                return Result.Fail($"Profession with '{request.ProfessionId}' id, does not exist");

            var language = await _languageService.GetByIdAsync(request.LanguageId);
            if (language is null)
                return Result.Fail($"Language with '{request.LanguageId}' id, does not exist");
            var languageKnowledge = new LanguageKnowledge(language, request.LanguageProficiencyLevel);

            var freelancer = new Freelancer(request.UserId, request.FirstName, request.LastName, request.Contact, request.IsProfilePublic,
                request.ProfileSummary, request.HourlyRate, request.Availability, request.ExperienceLevel, profession);
            freelancer.AddLanguageKnowledge(languageKnowledge);

            freelancer = await _freelancerRepository.CreateAsync(freelancer);
            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!result)
                return Result.Fail("Freelancer creation failed");
            return Result.Ok(freelancer);
        }

    }
}
