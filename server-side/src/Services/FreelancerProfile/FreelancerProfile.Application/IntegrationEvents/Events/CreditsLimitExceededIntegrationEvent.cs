using EventBus.Events;

namespace FreelancerProfile.Application.IntegrationEvents.Events
{
    public record CreditsLimitExceededIntegrationEvent : IntegrationEvent
    {
        public Guid JobId { get; init; }
        public Guid ProposalId { get; init; }

        public CreditsLimitExceededIntegrationEvent()
        {
        }

        public CreditsLimitExceededIntegrationEvent(Guid jobId, Guid proposalId)
        {
            JobId = jobId;
            ProposalId = proposalId;
        }
    }
}
