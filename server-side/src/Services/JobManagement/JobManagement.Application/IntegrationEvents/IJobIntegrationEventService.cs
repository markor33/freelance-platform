using EventBus.Events;

namespace JobManagement.Application.IntegrationEvents
{
    public interface IJobIntegrationEventService
    {
        Task SaveEventAsync(IntegrationEvent @event);
    }
}
