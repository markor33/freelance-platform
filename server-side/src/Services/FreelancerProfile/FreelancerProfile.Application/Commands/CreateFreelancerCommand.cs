using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Enums;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    [DataContract]
    public class CreateFreelancerCommand : IRequest<Result<string>>
    {
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string FirstName { get; private set; }
        [DataMember]
        public string LastName { get; private set; }
        [DataMember]
        public Contact Contact { get; private set; }
        [DataMember]
        public bool IsProfilePublic { get; private set; }
        [DataMember]
        public ProfileSummary ProfileSummary { get; private set; }
        [DataMember]
        public HourlyRate HourlyRate { get; private set; }
        [DataMember]
        public Availability Availability { get; private set; }
        [DataMember]
        public ExperienceLevel ExperienceLevel { get; private set; }
        [DataMember]
        public Guid ProfessionId { get; private set; }
        [DataMember]
        public int LanguageId { get; private set; }
        [DataMember]
        public LanguageProficiencyLevel LanguageProficiencyLevel { get; private set; }

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
