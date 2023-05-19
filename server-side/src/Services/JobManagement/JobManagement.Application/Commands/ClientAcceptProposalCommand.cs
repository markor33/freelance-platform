using FluentResults;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace JobManagement.Application.Commands
{
    [DataContract]
    public class ClientAcceptProposalCommand : IRequest<Result>
    {
        [DataMember]
        public Guid JobId { get; private set; }
        [DataMember]
        public Guid ProposalId { get; private set; }

        public ClientAcceptProposalCommand() { }

        [JsonConstructor]
        public ClientAcceptProposalCommand(Guid jobId, Guid proposalId)
        {
            JobId = jobId;
            ProposalId = proposalId;
        }

    }
}
