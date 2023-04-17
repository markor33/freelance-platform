﻿using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entites;
using MediatR;
using FluentResults;

namespace FreelancerProfile.Application.Commands
{
    public class CreateFreelancerCommandHandler : IRequestHandler<CreateFreelancerCommand, Result<string>>
    {
        private readonly IFreelancerRepository _freelancerRepository;
        private readonly ILanguageQueries _languageQeuries;
        private readonly IProfessionQueries _professionQueries;

        public CreateFreelancerCommandHandler(
            IFreelancerRepository freelancerRepository, 
            ILanguageQueries languageQeuries,
            IProfessionQueries professionQueries)
        {
            _freelancerRepository = freelancerRepository;
            _languageQeuries = languageQeuries;
            _professionQueries = professionQueries;
        }

        public async Task<Result<string>> Handle(CreateFreelancerCommand request, CancellationToken cancellationToken)
        {
            var profession = await _professionQueries.GetByIdAsync(request.ProfessionId);
            if (profession is null)
                return Result.Fail($"Profession with '{request.ProfessionId}' id, does not exist");

            var language = await _languageQeuries.GetByIdAsync(request.LanguageId);
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
            return Result.Ok("Freelancer created successfully");
        }

    }
}
