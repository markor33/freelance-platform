using FreelancerProfile.Domain.SeedWork;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class CertificationDeleted : DomainEvent, INotification
    {
        public Guid CertificationId { get; private set; }

        [JsonConstructor]
        public CertificationDeleted(Guid aggregateId, Guid certificationId) : base(aggregateId)
        {
            CertificationId = certificationId;
        }
    }
}
