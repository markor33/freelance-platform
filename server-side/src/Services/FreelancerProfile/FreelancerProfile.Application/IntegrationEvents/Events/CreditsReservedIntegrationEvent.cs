using EventBus.Events;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Application.IntegrationEvents.Events
{
    public record CreditsReservedIntegrationEvent : IntegrationEvent
    {
        public Guid JobId { get; init; }
        public Guid ProposalId { get; init; }

        public CreditsReservedIntegrationEvent()
        {
        }

        [JsonConstructor]
        public CreditsReservedIntegrationEvent(Guid jobId, Guid proposalId)
        {
            JobId = jobId;
            ProposalId = proposalId;
        }
    }
}
