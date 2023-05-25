using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    [DataContract]
    public class EditCertificationCommand : IRequest<Result<Certification>>
    {
        [DataMember]
        public Guid FreelancerId { get; set; }
        [DataMember]
        public Guid CertificationId { get; private set; }
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

        public EditCertificationCommand() { }

        [JsonConstructor]
        public EditCertificationCommand(Guid certificationId, string name, 
            string provider, string? description, DateTime start, DateTime end)
        {
            CertificationId = certificationId;
            Name = name;
            Provider = provider;
            if (string.IsNullOrEmpty(description)) description = null;
            Description = description;
            Start = start;
            End = end;
        }

    }
}
