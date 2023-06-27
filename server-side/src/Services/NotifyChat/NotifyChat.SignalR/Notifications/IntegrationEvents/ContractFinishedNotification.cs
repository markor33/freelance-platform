using EventBus.Events;
using System.Text.Json.Serialization;

namespace NotifyChat.Notifications.IntegrationEvents
{
    public record ContractFinishedNotification : IntegrationEvent
    {
        public Guid ContractId { get; private set; }
        public Guid FreelancerId { get; private set; }
        public Guid JobId { get; private set; }
        public string JobTitle { get; private set; }

        [JsonConstructor]
        public ContractFinishedNotification(Guid contractId, Guid freelancerId, Guid jobId, string jobTitle)
        {
            ContractId = contractId;
            FreelancerId = freelancerId;
            JobId = jobId;
            JobTitle = jobTitle;
        }
    }
}
