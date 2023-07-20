using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Enums;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;

namespace FreelancerProfile.Application.Queries
{
    [BsonCollection("Freelancers")]
    public class FreelancerViewModel
    {
        [BsonId]
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Contact Contact { get; private set; }
        public bool IsProfilePublic { get; private set; }
        public DateTime Joined { get; private set; }
        public ProfileSummary ProfileSummary { get; private set; }
        public List<LanguageKnowledge> LanguageKnowledges { get; private set; } = new();
        public HourlyRate HourlyRate { get; private set; }
        public Availability Availability { get; private set; }
        public ExperienceLevel ExperienceLevel { get; private set; }
        public string? ProfilePictureUrl { get; private set; }
        public ProfessionViewModel Profession { get; private set; }
        public List<SkillViewModel> Skills { get; private set; } = new();
        public List<EducationViewModel> Educations { get; private set; } = new();
        public List<CertificationViewModel> Certifications { get; private set; } = new();
        public List<EmploymentViewModel> Employments { get; private set; } = new();

        public FreelancerViewModel(Guid id, Guid userId, string firstName, string lastName, Contact contact)
        {
            Id = id;
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Contact = contact;
        }

        public void ProfileSetup(
            ProfileSummary profileSummary,
            HourlyRate hourlyRate,
            Availability availability,
            ExperienceLevel experienceLevel,
            ProfessionViewModel profession,
            LanguageKnowledge languageKnowledge)
        {
            ProfileSummary = profileSummary;
            HourlyRate = hourlyRate;
            Availability = availability;
            ExperienceLevel = experienceLevel;
            Profession = profession;
            LanguageKnowledges.Add(languageKnowledge);
        }

    }

    public class EducationViewModel
    {
        public Guid Id { get; private set; }
        public string SchoolName { get; private set; }
        public string Degree { get; private set; }
        public DateRange Attended { get; private set; }

        public EducationViewModel(Guid id, string schoolName, string degree, DateRange attended)
        {
            Id = id;
            SchoolName = schoolName;
            Degree = degree;
            Attended = attended;
        }
    }

    public class CertificationViewModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Provider { get; private set; }
        public DateRange Attended { get; private set; }
        public string Description { get; private set; }

        public CertificationViewModel(Guid id, string name, string provider, DateRange attended, string description)
        {
            Id = id;
            Name = name;
            Provider = provider;
            Attended = attended;
            Description = description;
        }
    }

    public class EmploymentViewModel
    {
        public Guid Id { get; private set; }
        public string Company { get; private set; }
        public string Title { get; private set; }
        public DateRange Period { get; private set; }
        public string Description { get; private set; }

        public EmploymentViewModel(Guid id, string company, string title, DateRange period, string description)
        {
            Id = id;
            Company = company;
            Title = title;
            Period = period;
            Description = description;
        }
    }

}
