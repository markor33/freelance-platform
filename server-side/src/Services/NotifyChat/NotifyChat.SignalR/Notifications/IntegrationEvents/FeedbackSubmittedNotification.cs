using EventBus.Events;

namespace NotifyChat.Notifications.IntegrationEvents
{
    public record FeedbackSubmittedNotification : IntegrationEvent
    {
        public Guid ContractId { get; set; }
        public Guid UserId { get; set; }

    }
}
