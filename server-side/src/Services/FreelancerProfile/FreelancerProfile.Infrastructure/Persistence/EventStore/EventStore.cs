﻿using FreelancerProfile.Domain.SeedWork;

namespace FreelancerProfile.Infrastructure.Persistence.EventStore
{
    public class EventStore : IEventStore
    {
        public async Task SaveEventsAsync(FreelancerProfileContext context)
        {
            var aggregates = context.ChangeTracker
                .Entries<EventSourcedAggregate>()
                .Where(x => x.Entity.Changes != null && x.Entity.Changes.Any());

            var domainEvents = aggregates.SelectMany(x => x.Entity.Changes).ToList();

            var domainEventLogs = domainEvents.Select(e => new DomainEventLog(e));

            await context.AddRangeAsync(domainEventLogs);
            await context.SaveChangesAsync();
        }
    }
}
