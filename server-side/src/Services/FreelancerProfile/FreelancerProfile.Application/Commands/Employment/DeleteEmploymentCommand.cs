using FluentResults;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    public class DeleteEmploymentCommand : IRequest<Result>
    {
        public Guid FreelancerId { get; private set; }
        public Guid EmploymentId { get; private set; }

        [JsonConstructor]
        public DeleteEmploymentCommand(Guid freelancerId, Guid employmentId)
        {
            FreelancerId = freelancerId;
            EmploymentId = employmentId;
        }
    }
}
