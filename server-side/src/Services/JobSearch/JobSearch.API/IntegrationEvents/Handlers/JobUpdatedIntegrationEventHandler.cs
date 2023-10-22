using EventBus.Abstractions;
using JobSearch.Abstractions;
using JobSearch.Abstractions.Model;
using JobSearch.API.IntegrationEvents.Events;

namespace JobSearch.API.IntegrationEvents.Handlers
{
    public class JobUpdatedIntegrationEventHandler : IIntegrationEventHandler<JobUpdatedIntegrationEvent>
    {
        private readonly IJobRepository _jobRepository;

        public JobUpdatedIntegrationEventHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task HandleAsync(JobUpdatedIntegrationEvent @event)
        {
            var job = new Job(
                @event.JobId, @event.ClientId, @event.Title,
                @event.Description, @event.CreationDate, @event.Credits,
                @event.ExperienceLevel, @event.Payment, @event.Status,
                @event.ProfessionId, @event.Skills);
            await _jobRepository.UpdateAsync(job);
        }
    }
}
