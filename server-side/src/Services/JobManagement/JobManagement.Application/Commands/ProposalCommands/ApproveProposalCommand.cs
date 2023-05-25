using FluentResults;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace JobManagement.Application.Commands.ProposalCommands
{
    [DataContract]
    public class ApproveProposalCommand : IRequest<Result>
    {
        [DataMember]
        public Guid JobId { get; private set; }
        [DataMember]
        public Guid ProposalId { get; private set; }

        public ApproveProposalCommand() { }

        [JsonConstructor]
        public ApproveProposalCommand(Guid jobId, Guid proposalId)
        {
            JobId = jobId;
            ProposalId = proposalId;
        }

    }
}
