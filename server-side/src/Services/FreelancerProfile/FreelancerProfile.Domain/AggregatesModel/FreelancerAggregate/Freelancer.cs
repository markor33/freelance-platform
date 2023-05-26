using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Enums;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using FreelancerProfile.Domain.SeedWork;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate
{
    public class Freelancer : Entity<Guid>, IAggregateRoot
    {
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set;}
        public Contact Contact { get; private set; }
        public DateTime Joined { get; private set; }
        public int Credits { get; private set; }
        public bool IsProfilePublic { get; private set; }
        public ProfileSummary ProfileSummary { get; private set; }
        public HourlyRate HourlyRate { get; private set; }
        public List<LanguageKnowledge> LanguageKnowledges { get; private set; }
        public Availability Availability { get; private set; }
        public ExperienceLevel ExperienceLevel { get; private set; }
        public Guid ProfessionId { get; private set; }
        public Profession Profession { get; private set; }
        public List<Skill> Skills { get; private set; }
        public List<Education> Educations { get; private set; }
        public List<Certification> Certifications { get; private set; }
        public List<Employment> Employments { get; private set; }
        public List<PortfolioProject> PortfolioProjects { get; private set; }

        public Freelancer()
        {
            Credits = 10;
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
            Joined = DateTime.Now;
            ProfileSummary = profileSummary;
            HourlyRate = hourlyRate;
            Availability = availability;
            ExperienceLevel = experienceLevel;
            Profession = profession;
            ProfessionId = profession.Id;
            LanguageKnowledges = new List<LanguageKnowledge>();
            Skills = new List<Skill>();
            AddDomainEvent(new FreelancerCreatedDomainEvent(this));
        }

        public void AddLanguageKnowledge(LanguageKnowledge languageKnowledge)
        {
            LanguageKnowledges.Add(languageKnowledge);
        }

        public Result AddSkill(Skill skill)
        {
            if (skill.ProfessionId != ProfessionId)
                return Result.Fail($"Freelancers profession does not contain {skill.Name} skill");

            if (Skills.Contains(skill))
                return Result.Fail($"Skill '{skill.Name}' already added");

            Skills.Add(skill);
            AddDomainEvent(new SkillAddedDomainEvent(Id, skill));
            return Result.Ok();
        }

        public Result AddSkill(List<Skill> skills)
        {
            foreach (var skill in skills)
            {
                var result = AddSkill(skill);
                if (result.IsFailed)
                    return result;
            }
            return Result.Ok();
        }

        public void AddEducation(Education education)
        {
            Educations.Add(education);
            AddDomainEvent(new EducationAddedDomainEvent(Id, education));
        }

        public Result<Education> UpdateEducation(Guid educationId, string schoolName, string degree, DateRange attended)
        {
            var education = Educations.FirstOrDefault(e => e.Id == educationId);
            if (education is null)
                return Result.Fail("Education does not exist");

            education.Update(schoolName, degree, attended);
            AddDomainEvent(new EducationEdittedDomainEvent(Id, education));

            return Result.Ok(education);
        }

        public Result DeleteEducation(Guid educationId)
        {
            var education = Educations.FirstOrDefault(e => e.Id == educationId);
            if (education is null)
                return Result.Fail("Education does not exist");

            Educations.Remove(education);
            AddDomainEvent(new EducationDeletedDomainEvent(Id, education.Id));

            return Result.Ok();
        }

        public void AddCertification(Certification certification)
        {
            Certifications.Add(certification);
            AddDomainEvent(new CertificationAddedDomainEvent(Id, certification));
        }

        public Result<Certification> UpdateCertification(Guid certificationId, string name, string provider, DateRange attended, string? description)
        {
            var certification = Certifications.FirstOrDefault(c => c.Id == certificationId);
            if (certification is null)
                return Result.Fail("Cerfitication does not exist");

            certification.Update(name, provider, attended, description);
            AddDomainEvent(new CertificationEdittedDomainEvent(Id, certification));

            return Result.Ok(certification);
        }

        public Result DeleteCertification(Guid certificationId)
        {
            var certification = Certifications.FirstOrDefault(c => c.Id == certificationId);
            if (certification is null)
                return Result.Fail("Cerfitication does not exist");

            Certifications.Remove(certification);
            AddDomainEvent(new CertificationDeletedDomainEvent(Id, certificationId));

            return Result.Ok();
        }

        public void AddEmployment(Employment employment)
        {
            Employments.Add(employment);
            AddDomainEvent(new EmploymentAddedDomainEvent(Id, employment));
        }

        public void AddPortfolioProject(PortfolioProject portfolioProject)
        {
            PortfolioProjects.Add(portfolioProject);
        }

        public bool SubtractCredits(int credits)
        {
            if (Credits < credits) return false;
            Credits -= credits;
            return true;
        }

    }
}
