using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.SeedWork;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class SkillsUpdated : DomainEvent, INotification
    {
        [JsonIgnore]
        public List<Skill> Skills { get; private set; }
        public List<Guid> SkillIds { get; private set; }

        public SkillsUpdated(Guid aggregateId, List<Skill> skills) : base(aggregateId)
        {
            Skills = skills;
            SkillIds = skills.Select(x => x.Id).ToList();
        }

        [JsonConstructor]
        public SkillsUpdated(Guid aggregateId, List<Guid> skillIds) : base(aggregateId)
        {
            SkillIds = skillIds;
            Skills = skillIds.Select(s => new Skill(s)).ToList();
        }
    }
}
