using EventBus.Abstractions;
using JobSearch.Abstractions;
using JobSearch.Abstractions.Model;
using JobSearch.API.IntegrationEvents.Events;

namespace JobSearch.API.IntegrationEvents.Handlers
{
    public class JobCreatedIntegrationEventHandler : IIntegrationEventHandler<JobCreatedIntegrationEvent>
    {
        private readonly IJobRepository _jobRepository;

        public JobCreatedIntegrationEventHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task HandleAsync(JobCreatedIntegrationEvent @event)
        {
            var job = new Job(
                @event.JobId, @event.ClientId, @event.Title, 
                @event.Description, @event.CreationDate, @event.Credits,
                @event.ExperienceLevel, @event.Payment, @event.Status, 
                @event.ProfessionId, @event.Skills);
            await _jobRepository.CreateAsync(job);
        }
    }
}
