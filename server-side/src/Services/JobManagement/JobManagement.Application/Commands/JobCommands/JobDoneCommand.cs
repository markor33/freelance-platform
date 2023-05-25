using FluentResults;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace JobManagement.Application.Commands.JobCommands
{
    [DataContract]
    public class JobDoneCommand : IRequest<Result>
    {
        [DataMember]
        public Guid JobId { get; private set; }

        public JobDoneCommand() { }

        [JsonConstructor]
        public JobDoneCommand(Guid jobId)
        {
            JobId = jobId;
        }
    }
}
