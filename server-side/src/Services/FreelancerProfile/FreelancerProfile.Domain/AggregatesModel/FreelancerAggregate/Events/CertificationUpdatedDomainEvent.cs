using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class CertificationUpdatedDomainEvent : INotification
    {
        public Guid FreelancerId { get; private set; }
        public Certification Certification { get; private set; }

        public CertificationUpdatedDomainEvent(Guid freelancerId, Certification certification)
        {
            FreelancerId = freelancerId;
            Certification = certification;
        }

    }
}
