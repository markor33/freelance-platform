using FreelancerProfile.Domain.SeedWork;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;

namespace JobManagement.Domain.AggregatesModel.JobAggregate.Entities
{
    public class Proposal : Entity<Guid>
    {
        public Guid FreelancerId { get; private set; }
        public string Text { get; private set; }
        public Payment Payment { get; private set; }
        public ProposalStatus? ProposalStatus { get; private set; }
        public List<Answer> Answers { get; private set; }

        public Proposal()
        {
            Answers = new List<Answer>();
        }

        public Proposal(Guid freelancerId, string text, Payment payment, ProposalStatus? proposalStatus = null)
        {
            FreelancerId = freelancerId;
            Text = text;
            Payment = payment;
            ProposalStatus = proposalStatus;
            Answers = new List<Answer>();
        }

        public void AddAnswer(Answer answer)
        {
            Answers.Add(answer);
        }

        public void ChangeStatus(ProposalStatus proposalStatus)
        {
            ProposalStatus = proposalStatus;
        }

    }
}
