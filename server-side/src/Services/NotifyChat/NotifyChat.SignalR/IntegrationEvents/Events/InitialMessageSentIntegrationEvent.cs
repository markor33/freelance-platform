using EventBus.Events;
using System.Text.Json.Serialization;

namespace NotifyChat.SignalR.IntegrationEvents.Events
{
    public record InitialMessageSentIntegrationEvent : IntegrationEvent
    {
        public Guid JobId { get; private set; }
        public Guid ProposalId { get; private set; }
        public Guid FreelancerId { get; private set; }

        [JsonConstructor]
        public InitialMessageSentIntegrationEvent(Guid jobId, Guid proposalId, Guid freelancerId)
        {
            JobId = jobId;
            ProposalId = proposalId;
            FreelancerId = freelancerId;
        }
    }
}
