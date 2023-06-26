using EventBus.Events;

namespace FreelancerProfile.Application.IntegrationEvents
{
    public interface IFreelancerProfileIntegrationEventService
    {
        Task SaveEventAsync(IntegrationEvent @event);
    }
}
