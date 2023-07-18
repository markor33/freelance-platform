using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Enums;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    public class ProfileSetupCommand : IRequest<Result<Freelancer>>
    {
        public Guid FreelancerId { get; set; }
        public Guid UserId { get; set; }
        public bool IsProfilePublic { get; private set; }
        public ProfileSummary ProfileSummary { get; private set; }
        public HourlyRate HourlyRate { get; private set; }
        public Availability Availability { get; private set; }
        public ExperienceLevel ExperienceLevel { get; private set; }
        public Guid ProfessionId { get;  private set; }
        public int LanguageId { get;  private set; }
        public LanguageProficiencyLevel LanguageProficiencyLevel { get; private set; }

        public ProfileSetupCommand() { }

        [JsonConstructor]
        public ProfileSetupCommand(bool isProfilePublic, ProfileSummary profileSummary, HourlyRate hourlyRate,
            Availability availability, ExperienceLevel experienceLevel, Guid professionId,
            int languageId, LanguageProficiencyLevel languageProficiencyLevel)
        {
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
