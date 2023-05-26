using MediatR;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class EducationDeletedDomainEvent : INotification
    {
        public Guid FreelancerId { get; private set; }
        public Guid EducationId { get; private set; }

        public EducationDeletedDomainEvent(Guid freelancerId, Guid educationId)
        {
            FreelancerId = freelancerId;
            EducationId = educationId;
        }

    }
}
