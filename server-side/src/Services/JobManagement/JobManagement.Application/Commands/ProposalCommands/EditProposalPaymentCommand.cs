using FluentResults;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace JobManagement.Application.Commands.ProposalCommands
{
    [DataContract]
    public class EditProposalPaymentCommand : IRequest<Result>
    {
        [DataMember]
        public Guid JobId { get; private set; }
        [DataMember]
        public Guid ProposalId { get; private set; }
        [DataMember]
        public Payment Payment { get; private set; }

        public EditProposalPaymentCommand() { }

        [JsonConstructor]
        public EditProposalPaymentCommand(Guid jobId, Guid proposalId, Payment payment)
        {
            JobId = jobId;
            ProposalId = proposalId;
            Payment = payment;
        }

    }
}
