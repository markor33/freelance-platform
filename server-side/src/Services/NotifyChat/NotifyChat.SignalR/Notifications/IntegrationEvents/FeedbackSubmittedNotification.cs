using EventBus.Events;
using System.Text.Json.Serialization;

namespace NotifyChat.Notifications.IntegrationEvents
{
    public record FeedbackSubmittedNotification : IntegrationEvent
    {
        public Guid ContractId { get; private init; }
        public Guid UserId { get; private init; }

        [JsonConstructor]
        public FeedbackSubmittedNotification(Guid contractId, Guid userId)
        {
            ContractId = contractId;
            UserId = userId;
        }
    }
}
