using EventBus.Events;
using System.Text.Json.Serialization;

namespace NotifyChat.Notifications.IntegrationEvents
{
    public record ProposalPaymentChangedNotification : IntegrationEvent
    {
        public Guid JobId { get; private init; }
        public string JobTitle { get; private init; }
        public Guid ProposalId { get; private init; }
        public Guid FreelancerId { get; private init; }

        [JsonConstructor]
        public ProposalPaymentChangedNotification(Guid jobId, string jobTitle, Guid proposalId, Guid freelancerId)
        {
            JobId = jobId;
            JobTitle = jobTitle;
            ProposalId = proposalId;
            FreelancerId = freelancerId;
        }
    }
}
