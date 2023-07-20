using FreelancerProfile.Domain.SeedWork;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class EducationDeleted : DomainEvent, INotification
    {
        public Guid EducationId { get; private set; }

        [JsonConstructor]
        public EducationDeleted(Guid aggregateId, Guid educationId) : base(aggregateId)
        {
            EducationId = educationId;
        }

    }
}
