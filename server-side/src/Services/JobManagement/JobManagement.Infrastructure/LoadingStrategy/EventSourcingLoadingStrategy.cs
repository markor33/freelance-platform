using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace JobManagement.Infrastructure.LoadingStrategy
{
    public class EventSourcingLoadingStrategy : IAggregateLoadingStrategy
    {
        private readonly JobManagementContext _context;

        public EventSourcingLoadingStrategy(JobManagementContext context)
        {
            _context = context;
        }

        public async Task<Job> GetByIdAsync(Guid id)
        {
            var domainEventLogs = await _context.DomainEventLogs
                .Where(d => d.AggregateId == id)
                .OrderBy(d => d.Created)
                .ToListAsync();

            var job = new Job();

            foreach (var domainEventLog in domainEventLogs)
            {
                var eventType = Type.GetType(domainEventLog.EventType);
                var domainEvent = (DomainEvent)JsonSerializer.Deserialize(domainEventLog.EventData, eventType);
                job.Apply(domainEvent);
            }

            _context.Entry(job).State = EntityState.Unchanged;
            job.Questions.ForEach(x => _context.Entry(x).State = EntityState.Unchanged);
            job.Proposals.ForEach(x => _context.Entry(x).State = EntityState.Unchanged);
            job.Skills.ForEach(x => _context.Entry(x).State = EntityState.Unchanged);
            job.Contracts.ForEach(x => _context.Entry(x).State = EntityState.Unchanged);

            return job;
        }

    }
}
