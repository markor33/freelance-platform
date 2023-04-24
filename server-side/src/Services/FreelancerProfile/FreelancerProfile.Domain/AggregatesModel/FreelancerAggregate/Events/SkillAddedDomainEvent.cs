using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class SkillAddedDomainEvent : INotification
    {
        public Guid FreelancerId { get; private set; }
        public Skill Skill { get; private set; }

        public SkillAddedDomainEvent(Guid freelancerId, Skill skill)
        {
            FreelancerId = freelancerId;
            Skill = skill;
        }
    }
}
