using MediatR;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class EmploymentDeletedDomainEvent : INotification
    {
        public Guid FreelancerId { get; private set; }

        public Guid EmploymentId { get; private set; }

        public EmploymentDeletedDomainEvent(Guid freelancerId, Guid employmentId)
        {
            FreelancerId = freelancerId;
            EmploymentId = employmentId;
        }

    }
}
