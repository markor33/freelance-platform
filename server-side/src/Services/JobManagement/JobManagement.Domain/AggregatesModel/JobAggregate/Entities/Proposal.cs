using FreelancerProfile.Domain.SeedWork;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;

namespace JobManagement.Domain.AggregatesModel.JobAggregate.Entities
{
    public class Proposal : Entity<Guid>
    {
        public Guid FreelancerUserId { get; private set; }
        public string Text { get; private set; }
        public Payment Payment { get; private set; }
        public ProposalStatus ProposalStatus { get; private set; }

        public Proposal() { }

        public Proposal(Guid freelancerUserId, string text, Payment payment, ProposalStatus proposalStatus)
        {
            FreelancerUserId = freelancerUserId;
            Text = text;
            Payment = payment;
            ProposalStatus = proposalStatus;
        }

    }
}
