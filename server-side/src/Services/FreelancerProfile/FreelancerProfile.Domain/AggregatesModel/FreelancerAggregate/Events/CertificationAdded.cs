using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.SeedWork;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class CertificationAdded : DomainEvent, INotification
    {
        public Certification Certification { get; private set; }

        [JsonConstructor]
        public CertificationAdded(Guid aggregateId, Certification certification) : base(aggregateId)
        {
            Certification = certification;
        }
    }
}
