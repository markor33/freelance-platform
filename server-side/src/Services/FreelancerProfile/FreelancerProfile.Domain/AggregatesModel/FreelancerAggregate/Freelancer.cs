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
        public string? ProfilePictureUrl { get; private set; }
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
            Educations = new List<Education>();
            Certifications = new List<Certification>();
            Employments = new List<Employment>();
            PortfolioProjects = new List<PortfolioProject>();
            AddDomainEvent(new FreelancerCreatedDomainEvent(this));
        }

        public void SetProfilePicture(string profilePictureUrl)
        {
            ProfilePictureUrl = profilePictureUrl;
            AddDomainEvent(new ProfilePictureChangedDomainEvent(Id, profilePictureUrl));
        }

        public void AddLanguageKnowledge(LanguageKnowledge languageKnowledge)
        {
            LanguageKnowledges.Add(languageKnowledge);
        }

        public void UpdateSkills(List<Skill> skills)
        {
            var skillsToRemove = Skills.Where(s => !skills.Any(ns => ns.Id == s.Id)).ToList();
            foreach (var skillToRemove in skillsToRemove)
                Skills.Remove(skillToRemove);

            var skillsToAdd = skills.Where(ns => !Skills.Any(s => s.Id == ns.Id)).ToList();
            Skills.AddRange(skillsToAdd);

            AddDomainEvent(new SkillsUpdatedDomainEvent(Id, Skills));
        }

        public void UpdateProfileSummary(ProfileSummary profileSummary)
        {
            ProfileSummary = profileSummary;
            AddDomainEvent(new ProfileSummaryUpdatedDomainEvent(Id, profileSummary));
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
            AddDomainEvent(new EducationUpdatedDomainEvent(Id, education));

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
            AddDomainEvent(new CertificationUpdatedDomainEvent(Id, certification));

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

        public Result<Employment> UpdateEmployment(Guid employmentId, string company, string title, DateRange period, string description)
        {
            var employment = Employments.FirstOrDefault(e => e.Id == employmentId);
            if (employment is null)
                return Result.Fail("Employment does not exist");

            employment.Update(company, title, period, description);
            AddDomainEvent(new EmploymentUpdatedDomainEvent(Id, employment));

            return Result.Ok(employment);
        }

        public Result DeleteEmployment(Guid employmentId)
        {
            var employment = Employments.FirstOrDefault(e => e.Id == employmentId);
            if (employment is null)
                return Result.Fail("Employment does not exist");

            Employments.Remove(employment);
            AddDomainEvent(new EmploymentDeletedDomainEvent(Id, employment.Id));

            return Result.Ok();
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
