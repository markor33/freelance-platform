using EventBus.Events;
using System.Text.Json.Serialization;

namespace JobManagement.Application.IntegrationEvents.Events
{
    public record CreditsLimitExceededIntegrationEvent : IntegrationEvent
    {
        public Guid JobId { get; init; }
        public Guid ProposalId { get; init; }

        public CreditsLimitExceededIntegrationEvent()
        {
        }

        [JsonConstructor]
        public CreditsLimitExceededIntegrationEvent(Guid jobId, Guid proposalId)
        {
            JobId = jobId;
            ProposalId = proposalId;
        }
    }
}
