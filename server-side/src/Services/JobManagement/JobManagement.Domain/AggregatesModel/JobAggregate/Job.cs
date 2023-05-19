using FluentResults;
using FreelancerProfile.Domain.SeedWork;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;

namespace JobManagement.Domain.AggregatesModel.JobAggregate
{
    public class Job : Entity<Guid>, IAggregateRoot
    {
        public Guid ClientId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Credits { get; private set; }
        public ExperienceLevel ExperienceLevel { get; private set; }
        public Payment Payment { get; private set; }
        public JobStatus Status { get; private set; }
        public List<Question> Questions { get; private set; }
        public List<Proposal> Proposals { get; private set; }
        public Guid ProfessionId { get; private set; }
        public Profession Profession { get; private set; }
        public List<Skill> Skills { get; private set; }

        public Job()
        {
            Questions = new List<Question>();
            Proposals = new List<Proposal>();
            Skills = new List<Skill>();
        }

        public Job(Guid clientId, string title, string description, ExperienceLevel experienceLevel, Payment payment, Profession profession)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            Title = title;
            Description = description;
            ExperienceLevel = experienceLevel;
            Payment = payment;
            Status = JobStatus.LISTED;
            Credits = EvaluateCredits();
            Profession = profession;
            ProfessionId = profession.Id;
            Questions = new List<Question>();
            Proposals = new List<Proposal>();
            Skills = new List<Skill>();
        }

        private int EvaluateCredits()
            => ((int)ExperienceLevel) + 1;

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }

        public void AddQuestions(List<Question> questions)
        {
            foreach (var question in questions)
                AddQuestion(question);
        }

        public Result AddSkill(Skill skill)
        {
            if (skill.ProfessionId != ProfessionId)
                return Result.Fail($"Freelancers profession does not contain {skill.Name} skill");

            if (Skills.Contains(skill))
                return Result.Fail($"Skill '{skill.Name}' already added");

            Skills.Add(skill);
            return Result.Ok();
        }

        public Result AddProposal(Proposal proposal)
        {
            var freelancerAlreadyApplied = Proposals.Any(p => p.FreelancerId == proposal.FreelancerId);
            if (freelancerAlreadyApplied)
                return Result.Fail("Freelancer already applied for this job");

            var hasAnswersForAllQuestions = Questions.All(q => proposal.Answers.Any(a => a.QuestionId == q.Id));
            if (!hasAnswersForAllQuestions)
                return Result.Fail("Some questions are not answered");

            Proposals.Add(proposal);
            return Result.Ok();
        }

        public Result ChangeProposalStatus(Guid proposalId, ProposalStatus status)
        {
            var proposal = Proposals.FirstOrDefault(p => p.Id == proposalId);
            if (proposal is null)
                return Result.Fail("Proposal does not exist");

            proposal.ChangeStatus(status);
            return Result.Ok();
        }

        public Proposal GetProposal(Guid proposalId) => Proposals.FirstOrDefault(p => p.Id == proposalId);

        public void RemoveProposal(Guid proposalId)
        {
            var proposal = Proposals.First(p => p.Id == proposalId);
            Proposals.Remove(proposal);
        }

        public Result Delete()
        {
            if (Status != JobStatus.LISTED)
                return Result.Fail("Job can't be deleted");
            Status = JobStatus.REMOVED;
            return Result.Ok();
        }

    }
}
