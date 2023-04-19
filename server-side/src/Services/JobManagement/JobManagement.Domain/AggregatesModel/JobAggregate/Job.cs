using FreelancerProfile.Domain.SeedWork;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;

namespace JobManagement.Domain.AggregatesModel.JobAggregate
{
    public class Job : Entity<Guid>, IAggregateRoot
    {
        public Guid ClientUserId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Credits { get; private set; }
        public ExperienceLevel ExperienceLevel { get; private set; }
        public Payment Payment { get; private set; }
        public JobStatus JobStatus { get; private set; }
        public List<Proposal> Proposals { get; private set; }

        public Job()
        {
            Proposals = new List<Proposal>();
        }

        public Job(Guid clientUserId, string title, string description)
        {
            Id = Guid.NewGuid();
            ClientUserId = clientUserId;
            Title = title;
            Description = description;
            Proposals = new List<Proposal>();
        }

        public void AddProposal(Proposal proposal)
        {
            Proposals.Add(proposal);
        }

    }
}
