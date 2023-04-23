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
        public JobStatus JobStatus { get; private set; }
        public List<Question> Questions { get; private set; }
        public List<Proposal> Proposals { get; private set; }

        public Job()
        {
            Questions = new List<Question>();
            Proposals = new List<Proposal>();
        }

        public Job(Guid clientId, string title, string description, ExperienceLevel experienceLevel, Payment payment)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            Title = title;
            Description = description;
            ExperienceLevel = experienceLevel;
            Payment = payment;
            JobStatus = JobStatus.LISTED;
            Credits = EvaluateCredits();
            Questions = new List<Question>();
            Proposals = new List<Proposal>();
        }

        private int EvaluateCredits()
            => ((int)ExperienceLevel) + 1;

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
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

        public void RemoveProposal(Guid proposalId)
        {
            var proposal = Proposals.First(p => p.Id == proposalId);
            Proposals.Remove(proposal);
        }

    }
}
