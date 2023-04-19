using MediatR;
using FluentResults;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JobManagement.Domain.AggregatesModel.JobAggregate;

namespace JobManagement.Application.Commands
{
    [DataContract]
    public class CreateJobCommand : IRequest<Result<Job>>
    {
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string Title { get; private set; }
        [DataMember]
        public string Description { get; private set; } 

        public CreateJobCommand() { }

        [JsonConstructor]
        public CreateJobCommand(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
