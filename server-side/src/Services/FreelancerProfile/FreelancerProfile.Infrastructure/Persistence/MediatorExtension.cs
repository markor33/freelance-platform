using FreelancerProfile.Domain.SeedWork;
using MediatR;

namespace FreelancerProfile.Infrastructure.Persistence
{
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, FreelancerProfileContext ctx)
        {
            var aggregates = ctx.ChangeTracker
                .Entries<EventSourcedAggregate>()
                .Where(x => x.Entity.Changes != null && x.Entity.Changes.Any());

            var domainEvents = aggregates.SelectMany(x => x.Entity.Changes).ToList();

            aggregates.ToList().ForEach(a => a.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents.OfType<INotification>())
                await mediator.Publish(domainEvent);
        }
    }
}
