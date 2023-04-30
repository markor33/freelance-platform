using FluentResults;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace JobManagement.Application.Commands
{
    [DataContract]
    public class DeleteJobCommand : IRequest<Result>
    {
        [DataMember]
        public Guid JobId { get; private set; }

        public DeleteJobCommand() { }

        [JsonConstructor]
        public DeleteJobCommand(Guid jobId)
        {
            JobId = jobId;
        }
    }
}
