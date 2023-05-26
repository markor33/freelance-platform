using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class EmploymentEdittedDomainEvent : INotification
    {
        public Guid FreelancerId { get; private set; }
        public Employment Employment { get; private set; }

        public EmploymentEdittedDomainEvent(Guid freelancerId, Employment employment)
        {
            FreelancerId = freelancerId;
            Employment = employment;
        }

    }
}
