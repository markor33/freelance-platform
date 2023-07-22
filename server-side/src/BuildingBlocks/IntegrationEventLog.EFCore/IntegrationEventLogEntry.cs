using EventBus.Events;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace IntegrationEventLog.EFCore
{
    public class IntegrationEventLogEntry
    {
        private static readonly JsonSerializerOptions s_indentedOptions = new() { WriteIndented = true };
        private static readonly JsonSerializerOptions s_caseInsensitiveOptions = new() { PropertyNameCaseInsensitive = true };

        public Guid EventId { get; private set; }
        public string EventTypeName { get; private set; }
        [NotMapped]
        public string EvenTypeShortName => EventTypeName.Split('.').Last();
        public EventState State { get; set; }
        public DateTime CreationTime { get; private set; }
        public string Data { get; private set; }
        [NotMapped]
        public IntegrationEvent IntegrationEvent { get; private set; }

        public IntegrationEventLogEntry() { }

        public IntegrationEventLogEntry(IntegrationEvent @event)
        {
            EventId = @event.Id;
            CreationTime = @event.CreationDate;
            EventTypeName = @event.GetType().FullName;
            Data = JsonSerializer.Serialize(@event, @event.GetType(), s_indentedOptions);
        }

        public IntegrationEventLogEntry DeserializeJsonContent(Type type)
        {
            try {
                IntegrationEvent = JsonSerializer.Deserialize(Data, type, s_caseInsensitiveOptions) as IntegrationEvent;
            }
            catch (Exception ex)
            {
                var a = 1;
            }
            return this;
        }

    }
}
