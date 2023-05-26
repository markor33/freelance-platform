using FluentResults;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    public class DeleteCertificationCommand : IRequest<Result>
    {
        public Guid FreelancerId { get; private set; }
        public Guid CertificationId { get; private set; }

        [JsonConstructor]
        public DeleteCertificationCommand(Guid freelancerId, Guid certificationId)
        {
            FreelancerId = freelancerId;
            CertificationId = certificationId;
        }
    }
}
