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
        public ProposalStatus? Status { get; private set; }
        public List<Answer> Answers { get; private set; }
        public DateTime Created { get; private set; }

        public Proposal()
        {
            Answers = new List<Answer>();
        }

        public Proposal(Guid freelancerId, string text, Payment payment, ProposalStatus? proposalStatus = null)
        {
            FreelancerId = freelancerId;
            Text = text;
            Payment = payment;
            Status = proposalStatus;
            Answers = new List<Answer>();
            Created = DateTime.Now;
        }

        public void AddAnswer(Answer answer)
        {
            Answers.Add(answer);
        }

        public void ChangeStatus(ProposalStatus proposalStatus)
        {
            Status = proposalStatus;
        }

    }
}
