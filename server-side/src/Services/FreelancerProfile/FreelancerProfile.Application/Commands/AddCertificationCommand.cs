using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    [DataContract]
    public class AddCertificationCommand : IRequest<Result<Certification>>
    {
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string Provider { get; private set; }
        [DataMember]
        public string? Description { get; private set; }
        [DataMember]
        public DateTime Start { get; private set; }
        [DataMember]
        public DateTime End { get; private set; }

        public AddCertificationCommand() { }

        [JsonConstructor]
        public AddCertificationCommand(string name, string provider, string description, DateTime start, DateTime end)
        {
            Name = name;
            Provider = provider;
            if (string.IsNullOrEmpty(description)) description = null;
            Description = description;
            Start = start;
            End = end;
        }

    }
}
