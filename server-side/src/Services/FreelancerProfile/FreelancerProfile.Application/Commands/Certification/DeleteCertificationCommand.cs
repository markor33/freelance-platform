using FluentResults;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    [DataContract]
    public class DeleteCertificationCommand : IRequest<Result>
    {
        [DataMember]
        public Guid FreelancerId { get; private set; }
        [DataMember]
        public Guid CertificationId { get; private set; }

        public DeleteCertificationCommand() { }

        [JsonConstructor]
        public DeleteCertificationCommand(Guid freelancerId, Guid certificationId)
        {
            FreelancerId = freelancerId;
            CertificationId = certificationId;
        }
    }
}
