using EventBus.Events;
using System.Text.Json.Serialization;

namespace JobSearch.API.IntegrationEvents.Events
{
    public record JobDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid JobId { get; private set; }

        public JobDeletedIntegrationEvent() { }

        [JsonConstructor]
        public JobDeletedIntegrationEvent(Guid jobId)
        {
            JobId = jobId;
        }

    }
}
