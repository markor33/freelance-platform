using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class SkillsUpdatedDomainEvent : INotification
    {
        public Guid FreelancerId { get; private set; }
        public List<Skill> Skills { get; private set; }

        public SkillsUpdatedDomainEvent(Guid freelancerId, List<Skill> skills)
        {
            FreelancerId = freelancerId;
            Skills = skills;
        }
    }
}
