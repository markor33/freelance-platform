using FluentResults;
using MediatR;
using System.Runtime.Serialization;

namespace JobManagement.Application.Commands
{
    [DataContract]
    public class FinishContractCommand : IRequest<Result>
    {
        [DataMember]
        public Guid JobId { get; private set; }
        [DataMember]
        public Guid ContractId { get; private set; }

        public FinishContractCommand() { }

        public FinishContractCommand(Guid jobId, Guid contractId)
        {
            JobId = jobId;
            ContractId = contractId;
        }
    }
}
