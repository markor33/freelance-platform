using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entites;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Enums;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using FreelancerProfile.Domain.Exceptions;
using FreelancerProfile.Domain.SeedWork;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate
{
    public class Freelancer : Entity<Guid>, IAggregateRoot
    {
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set;}
        public Contact Contact { get; private set; }
        public bool IsProfilePublic { get; private set; }
        public ProfileSummary ProfileSummary { get; private set; }
        public HourlyRate HourlyRate { get; private set; }
        public List<LanguageKnowledge> LanguageKnowledges { get; private set; }
        public Availability Availability { get; private set; }
        public ExperienceLevel ExperienceLevel { get; private set; }
        public Profession Profession { get; private set; }
        public List<Skill> Skills { get; private set; }
        public List<Education> Educations { get; private set; }
        public List<Certification> Certifications { get; private set; }
        public List<Employment> Employments { get; private set; }
        public List<PortfolioProject> PortfolioProjects { get; private set; }

        public Freelancer()
        {
            LanguageKnowledges = new List<LanguageKnowledge>();
            Skills = new List<Skill>();
            Educations = new List<Education>();
            Certifications = new List<Certification>();
            Employments = new List<Employment>();
            PortfolioProjects = new List<PortfolioProject>();
        }

        public Freelancer(
            Guid userId, 
            string firstName, 
            string lastName, 
            Contact contact,
            bool isProfilePublic,
            ProfileSummary profileSummary,
            HourlyRate hourlyRate,
            Availability availability,
            ExperienceLevel experienceLevel,
            Profession profession)
        {
            Id = new Guid();
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Contact = contact;
            IsProfilePublic = isProfilePublic;
            ProfileSummary = profileSummary;
            HourlyRate = hourlyRate;
            Availability = availability;
            ExperienceLevel = experienceLevel;
            Profession = profession;
            LanguageKnowledges = new List<LanguageKnowledge>();
        }

        public void AddLanguageKnowledge(LanguageKnowledge languageKnowledge)
        {
            LanguageKnowledges.Add(languageKnowledge);
        }

        public void AddSkill(Skill skill)
        {
            if (skill.Profession != Profession)
                throw new FreelancerProfileDomainException($"Freelancer profession({Profession.Name}) does not contain {skill.Name} skill");
            Skills.Add(skill);
        }

        public void AddSkill(List<Skill> skills)
        {
            foreach(var skill in skills)
                AddSkill(skill);
        }

        public void AddEducation(Education education)
        {
            Educations.Add(education);
        }

        public void AddCertification(Certification certification)
        {
            Certifications.Add(certification);
        }

        public void AddEmployment(Employment employment)
        {
            Employments.Add(employment);
        }

        public void AddPortfolioProject(PortfolioProject portfolioProject)
        {
            PortfolioProjects.Add(portfolioProject);
        }

    }
}
