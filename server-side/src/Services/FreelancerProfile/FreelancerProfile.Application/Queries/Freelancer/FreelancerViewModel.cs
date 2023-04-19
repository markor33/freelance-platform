using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Enums;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;

namespace FreelancerProfile.Application.Queries
{
    public class FreelancerViewModel
    {
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Contact Contact { get; private set; }
        public bool IsProfilePublic { get; private set; }
        public DateTime Joined { get; private set; }
        public ProfileSummary ProfileSummary { get; private set; }
        public List<LanguageKnowledge> LanguageKnowledges { get; private set; }
        public HourlyRate HourlyRate { get; private set; }
        public Availability Availability { get; private set; }
        public ExperienceLevel ExperienceLevel { get; private set; }
        public ProfessionViewModel Profession { get; private set; }
        public List<SkillViewModel> Skills { get; private set; }
        public List<EducationViewModel> Educations { get; private set; }
        public List<CertificationViewModel> Certifications { get; private set; }
        public List<EmploymentViewModel> Employments { get; private set; }
    }

    public class EducationViewModel
    {
        public Guid Id { get; private set; }
        public string SchoolName { get; private set; }
        public string Degree { get; private set; }
        public DateRange Attended { get; private set; }
    }

    public class CertificationViewModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Provider { get; private set; }
        public DateRange Attended { get; private set; }
        public string Description { get; private set; }
    }

    public class EmploymentViewModel
    {
        public Guid Id { get; private set; }
        public string Company { get; private set; }
        public string Title { get; private set; }
        public DateRange Period { get; private set; }
        public string Description { get; private set; }
    }

}
