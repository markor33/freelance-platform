using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    [DataContract]
    public class AddSkillCommand : IRequest<Result<List<Skill>>>
    {
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public List<Guid> Skills { get; private set; }

        public AddSkillCommand() { }

        [JsonConstructor]
        public AddSkillCommand(List<Guid> skills)
        {
            Skills = skills;
        }

    }
}
