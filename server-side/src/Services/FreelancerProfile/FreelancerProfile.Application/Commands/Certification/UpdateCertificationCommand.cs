using FluentResults;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    public class UpdateCertificationCommand : IRequest<Result>
    {
        public Guid FreelancerId { get; set; }
        public Guid CertificationId { get; private set; }
        public string Name { get; private set; }
        public string Provider { get; private set; }
        public string? Description { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        [JsonConstructor]
        public UpdateCertificationCommand(Guid certificationId, string name, 
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
