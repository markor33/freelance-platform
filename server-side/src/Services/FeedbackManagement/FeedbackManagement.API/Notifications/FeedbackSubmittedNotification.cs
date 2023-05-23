using EventBus.Events;

namespace FeedbackManagement.API.Notifications
{
    public record FeedbackSubmittedNotification : IntegrationEvent
    {
        public Guid ContractId { get; set; }
        public Guid UserId { get; set; }

        public FeedbackSubmittedNotification(Guid contractId)
        {
            ContractId = contractId;
        }

    }
}
