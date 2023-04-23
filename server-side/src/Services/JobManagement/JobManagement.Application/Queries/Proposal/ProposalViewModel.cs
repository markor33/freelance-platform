using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;

namespace JobManagement.Application.Queries
{
    public record ProposalViewModel
    {
        public Guid Id { get; private init; }
        public string Text { get; private init; }
        public Payment Payment { get; private init; }
        public ProposalStatus Status { get; private init; }

        public ProposalViewModel() { }

        public ProposalViewModel(Guid id, string text, Payment payment, ProposalStatus status)
        {
            Id = id;
            Text = text;
            Payment = payment;
            Status = status;
        }
    }
}
