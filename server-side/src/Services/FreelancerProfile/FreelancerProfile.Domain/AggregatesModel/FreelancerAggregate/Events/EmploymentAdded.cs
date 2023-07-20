using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.SeedWork;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class EmploymentAdded : DomainEvent, INotification
    {
        public Employment Employment { get; private set; }

        [JsonConstructor]
        public EmploymentAdded(Guid aggregateId, Employment employment) : base(aggregateId)
        {
            Employment = employment;
        }
    }
}
