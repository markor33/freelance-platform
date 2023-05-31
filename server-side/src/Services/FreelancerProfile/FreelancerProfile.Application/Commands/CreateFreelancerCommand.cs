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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Contact Contact { get; set; }
        public bool IsProfilePublic { get; set; }
        public ProfileSummary ProfileSummary { get; set; }
        public HourlyRate HourlyRate { get; set; }
        public Availability Availability { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public Guid ProfessionId { get;  set; }
        public int LanguageId { get;  set; }
        public LanguageProficiencyLevel LanguageProficiencyLevel { get;  set; }

        public CreateFreelancerCommand() { }

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
