using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;

namespace JobManagement.Application.Queries
{
    public record ProposalViewModel
    {
        public Guid Id { get; private init; }
        public Guid FreelancerId { get; private init; }
        public string Text { get; private init; }
        public Payment Payment { get; set; }
        public ProposalStatus Status { get; private init; }

        public ProposalViewModel() { }

        public ProposalViewModel(Guid id, Guid freelancerId, string text, Payment payment, ProposalStatus status)
        {
            Id = id;
            FreelancerId = freelancerId;
            Text = text;
            Payment = payment;
            Status = status;
        }
    }
}
