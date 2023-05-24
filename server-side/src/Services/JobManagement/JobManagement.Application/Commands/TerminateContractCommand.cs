using FluentResults;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace JobManagement.Application.Commands
{
    [DataContract]
    public class TerminateContractCommand : IRequest<Result>
    {
        [DataMember]
        public Guid JobId { get; set; }
        [DataMember]
        public Guid ContractId { get; private set; }

        public TerminateContractCommand() { }

        [JsonConstructor]
        public TerminateContractCommand(Guid jobId, Guid contractId)
        {
            JobId = jobId;
            ContractId = contractId;
        }

    }
}
