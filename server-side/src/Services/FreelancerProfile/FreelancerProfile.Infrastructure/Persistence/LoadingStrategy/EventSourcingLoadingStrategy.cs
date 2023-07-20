using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FreelancerProfile.Infrastructure.Persistence.LoadingStrategy
{
    public class EventSourcingLoadingStrategy : IAggregateLoadingStrategy
    {
        private readonly FreelancerProfileContext _context;

        public EventSourcingLoadingStrategy(FreelancerProfileContext context)
        {
            _context = context;
        }

        public async Task<Freelancer> GetByIdAsync(Guid id)
        {
            var domainEventLogs = await _context.DomainEventLogs
                .Where(d => d.AggregateId == id)
                .OrderBy(d => d.Created)
                .ToListAsync();

            var freelancer = new Freelancer();

            foreach (var domainEventLog in domainEventLogs)
            {
                var eventType = Type.GetType(domainEventLog.EventType);
                var domainEvent = (DomainEvent)JsonSerializer.Deserialize(domainEventLog.EventData, eventType);
                freelancer.Apply(domainEvent);
            }

            _context.Entry(freelancer).State = EntityState.Unchanged;

            return freelancer;
        }

    }
}
