using EventBus.Events;

namespace FreelancerProfile.Application.IntegrationEvents.Events
{
    public record CreditsReservedIntegrationEvent : IntegrationEvent
    {
        public Guid JobId { get; init; }
        public Guid ProposalId { get; init; }

        public CreditsReservedIntegrationEvent(Guid jobId, Guid proposalId)
        {
            JobId = jobId;
            ProposalId = proposalId;
        }
    }
}
