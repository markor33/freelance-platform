using FluentResults;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    public class DeleteEducationCommand : IRequest<Result>
    {
        public Guid FreelancerId { get; private set; }
        public Guid EducationId { get; private set; }

        [JsonConstructor]
        public DeleteEducationCommand(Guid freelancerId, Guid educationId)
        {
            FreelancerId = freelancerId;
            EducationId = educationId;
        }
    }
}
