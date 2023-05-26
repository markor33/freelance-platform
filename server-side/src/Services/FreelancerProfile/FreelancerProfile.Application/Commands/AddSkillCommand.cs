using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.Commands
{
    public class AddSkillCommand : IRequest<Result<List<Skill>>>
    {
        public Guid UserId { get; set; }
        public List<Guid> Skills { get; private set; }

        [JsonConstructor]
        public AddSkillCommand(List<Guid> skills)
        {
            Skills = skills;
        }

    }
}
