using FreelancerProfile.Domain.SeedWork;

namespace FreelancerProfile.Infrastructure.Persistence.EventStore
{
    public interface IEventStore
    {
        Task SaveEventsAsync(FreelancerProfileContext context);
    }
}
