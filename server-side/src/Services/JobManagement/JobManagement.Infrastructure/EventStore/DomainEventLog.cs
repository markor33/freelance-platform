using JobManagement.Domain.SeedWork;
using System.Text.Json;

namespace JobManagement.Infrastructure.EventStore
{
    public class DomainEventLog
    {
        public Guid Id { get; private set; }
        public Guid AggregateId { get; private set; }
        public string EventType { get; private set; }
        public string EventData { get; private set; }
        public DateTime Created { get; private set; }

        public DomainEventLog()
        {
        }

        public DomainEventLog(DomainEvent @event)
        {
            Id = Guid.NewGuid();
            AggregateId = @event.AggregateId;
            EventType = @event.GetType().AssemblyQualifiedName;
            EventData = JsonSerializer.Serialize(@event, @event.GetType());
            Created = DateTime.UtcNow;
        }

    }
}
