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
        public DateTime Created { get; private set; }
        public int Credits { get; private set; }
        public ExperienceLevel ExperienceLevel { get; private set; }
        public Payment Payment { get; private set; }
        public JobStatus Status { get; private set; }
        public List<Question> Questions { get; private set; }
        public List<Proposal> Proposals { get; private set; }
        public Guid ProfessionId { get; private set; }
        public Profession Profession { get; private set; }
        public List<Skill> Skills { get; private set; }
        public List<Contract> Contracts { get; private set; }

        public Job()
        {
            Questions = new List<Question>();
            Proposals = new List<Proposal>();
            Skills = new List<Skill>();
            Contracts = new List<Contract>();
        }

        public Job(Guid clientId, string title, string description, ExperienceLevel experienceLevel, 
            Payment payment, Profession profession, List<Question> questions, List<Skill> skills)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            Title = title;
            Created = DateTime.UtcNow;
            Description = description;
            ExperienceLevel = experienceLevel;
            Payment = payment;
            Status = JobStatus.LISTED;
            Credits = EvaluateCredits();
            Profession = profession;
            ProfessionId = profession.Id;
            Questions = questions;
            Skills = skills;
            Proposals = new List<Proposal>();
            Contracts = new List<Contract>();
        }

        public void Update(string title, string description, ExperienceLevel experienceLevel,
            Payment payment, Profession profession, List<Question> questions, List<Skill> skills)
        {
            Title = title;
            Description = description;
            ExperienceLevel = experienceLevel;
            Payment = payment;
            Credits = EvaluateCredits();
            Profession = profession;
            ProfessionId = profession.Id;
            UpdateSkills(skills);
            UpdateQuestions(questions);
        }

        private void UpdateSkills(List<Skill> skills)
        {
            var skillsToRemove = Skills.Where(s => !skills.Any(ns => ns.Id == s.Id)).ToList();
            foreach (var skillToRemove in skillsToRemove)
                Skills.Remove(skillToRemove);

            var skillsToAdd = skills.Where(ns => !Skills.Any(s => s.Id == ns.Id)).ToList();
            Skills.AddRange(skillsToAdd);
        }

        private void UpdateQuestions(List<Question> questions)
        {
            var questionsToRemove = Questions.Where(q => !questions.Any(nq => nq.Id == q.Id)).ToList();
            foreach (var questionToRemove in questionsToRemove)
                Questions.Remove(questionToRemove);

            var existingQuestions = Questions.Where(q => questions.Any(nq => nq.Id == q.Id && nq.Text != q.Text)).ToList();
            foreach (var existingQuestion in existingQuestions)
            {
                var newQuestion = questions.First(nq => nq.Id == existingQuestion.Id);
                existingQuestion.SetText(newQuestion.Text);
            }

            var questionsToAdd = questions.Where(nq => !Questions.Any(q => q.Id == nq.Id)).ToList();
            Questions.AddRange(questionsToAdd);

        }

        private int EvaluateCredits() => ((int)ExperienceLevel) + 1;

        public Result<Contract> MakeContract(Guid proposalId)
        {
            var proposal = GetProposal(proposalId);
            if (proposal is null || proposal.Status != ProposalStatus.CLIENT_APPROVED)
                return Result.Fail("Proposal does not exist or it's not approved");

            var newContract = new Contract(ClientId, proposal.FreelancerId, proposal.Payment);
            Contracts.Add(newContract);
            proposal.ChangeStatus(ProposalStatus.FREELANCER_APPROVED);
            Status = JobStatus.IN_PROGRESS;

            return Result.Ok(newContract);
        }

        public Result<Contract> ChangeContractStatus(Guid id, ContractStatus status)
        {
            var contract = Contracts.FirstOrDefault(c => c.Id == id);
            if (contract is null)
                return Result.Fail("Contract does not exist");

            contract.ChangeStatus(status);
            return Result.Ok(contract);
        }

        public Contract GetContract(Guid contractId) => Contracts.FirstOrDefault(c => c.Id == contractId);

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

        public Result ChangeProposalStatus(Guid id, ProposalStatus status)
        {
            var proposal = GetProposal(id);
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

        public Result Done()
        {
            if (!Contracts.All(c => c.Status != ContractStatus.ACTIVE))
                return Result.Fail("Job can't be done. Active contracts exist.");

            Proposals.Clear();
            Status = JobStatus.DONE;
            return Result.Ok();
        }

        public Result Delete()
        {
            if (Contracts.Any())
                return Result.Fail("Job can't be deleted. Active contracts exist.");

            Proposals.Clear();
            Status = JobStatus.REMOVED;
            return Result.Ok();
        }

    }
}
