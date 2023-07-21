using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.SeedWork;
using System.Text.Json.Serialization;

namespace JobManagement.Domain.AggregatesModel.JobAggregate.Events
{
    public class ProposalCreated : DomainEvent
    {
        public Proposal Proposal { get; private set; }

        [JsonConstructor]
        public ProposalCreated(Guid aggregateId, Proposal proposal) : base(aggregateId)
        {
            Proposal = proposal;
        }
    }
}
