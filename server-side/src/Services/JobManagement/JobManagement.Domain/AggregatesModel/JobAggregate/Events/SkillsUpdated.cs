using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.SeedWork;
using System.Text.Json.Serialization;

namespace JobManagement.Domain.AggregatesModel.JobAggregate.Events
{
    public class SkillsUpdated : DomainEvent
    {
        public List<Skill> Skills { get; private set; }

        [JsonConstructor]
        public SkillsUpdated(Guid aggregateId, List<Skill> skills) : base(aggregateId)
        {
            Skills = skills;
        }

    }
}
