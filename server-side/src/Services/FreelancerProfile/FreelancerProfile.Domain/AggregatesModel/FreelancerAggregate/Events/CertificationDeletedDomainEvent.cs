using MediatR;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class CertificationDeletedDomainEvent : INotification
    {
        public Guid FreelancerId { get; private set; }
        public Guid Certificationid { get; private set; }

        public CertificationDeletedDomainEvent(Guid freelanderId, Guid certificationid)
        {
            FreelancerId = freelanderId;
            Certificationid = certificationid;
        }
    }
}
