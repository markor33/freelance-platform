using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Enums;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    public class CreateFreelancerCommand : IRequest<Result<Freelancer>>
    {
        public Guid UserId { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Contact Contact { get; private set; }
        public bool IsProfilePublic { get; private set; }
        public ProfileSummary ProfileSummary { get; private set; }
        public HourlyRate HourlyRate { get; private set; }
        public Availability Availability { get; private set; }
        public ExperienceLevel ExperienceLevel { get; private set; }
        public Guid ProfessionId { get; private set; }
        public int LanguageId { get; private set; }
        public LanguageProficiencyLevel LanguageProficiencyLevel { get; private set; }

        [JsonConstructor]
        public CreateFreelancerCommand(Guid userId, string firstName, string lastName,
            Contact contact, bool isProfilePublic, ProfileSummary profileSummary, HourlyRate hourlyRate,
            Availability availability, ExperienceLevel experienceLevel, Guid professionId,
            int languageId, LanguageProficiencyLevel languageProficiencyLevel)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Contact = contact;
            IsProfilePublic = isProfilePublic;
            ProfileSummary = profileSummary;
            HourlyRate = hourlyRate;
            Availability = availability;
            ExperienceLevel = experienceLevel;
            ProfessionId = professionId;
            LanguageId = languageId;
            LanguageProficiencyLevel = languageProficiencyLevel;
        }

    }
}
