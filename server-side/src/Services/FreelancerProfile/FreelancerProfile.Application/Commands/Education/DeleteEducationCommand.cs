using FluentResults;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    [DataContract]
    public class DeleteEducationCommand : IRequest<Result>
    {
        [DataMember]
        public Guid FreelancerId { get; private set; }
        [DataMember]
        public Guid EducationId { get; private set; }

        public DeleteEducationCommand() { }

        [JsonConstructor]
        public DeleteEducationCommand(Guid freelancerId, Guid educationId)
        {
            FreelancerId = freelancerId;
            EducationId = educationId;
        }
    }
}
