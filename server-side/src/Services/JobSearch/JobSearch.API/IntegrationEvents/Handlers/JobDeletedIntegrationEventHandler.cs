using EventBus.Abstractions;
using JobSearch.Abstractions;
using JobSearch.API.IntegrationEvents.Events;

namespace JobSearch.API.IntegrationEvents.Handlers
{
    public class JobDeletedIntegrationEventHandler : IIntegrationEventHandler<JobDeletedIntegrationEvent>
    {
        private readonly IJobRepository _jobRepository;

        public JobDeletedIntegrationEventHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task HandleAsync(JobDeletedIntegrationEvent @event)
        {
            await _jobRepository.DeleteAsync(@event.JobId);
        }

    }
}
