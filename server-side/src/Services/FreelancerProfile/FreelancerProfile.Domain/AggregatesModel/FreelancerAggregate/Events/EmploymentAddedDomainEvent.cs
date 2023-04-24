using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class EmploymentAddedDomainEvent : INotification
    {
        public Guid FreelancerId { get; private set; }
        public Employment Employment { get; private set; }

        public EmploymentAddedDomainEvent(Guid freelancerId, Employment employment)
        {
            FreelancerId = freelancerId;
            Employment = employment;
        }
    }
}
