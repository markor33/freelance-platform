using EventBus.Events;

namespace NotifyChat.SignalR.IntegrationEvents.Events
{
    public record InitialMessageSentIntegrationEvent : IntegrationEvent
    {
        public Guid JobId { get; private set; }
        public Guid ProposalId { get; private set; }
        public Guid FreelancerId { get; private set; }

        public InitialMessageSentIntegrationEvent(Guid jobId, Guid proposalId, Guid freelancerId)
        {
            JobId = jobId;
            ProposalId = proposalId;
            FreelancerId = freelancerId;
        }
    }
}
