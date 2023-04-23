using EventBus.Abstractions;
using JobManagement.Application.IntegrationEvents.Events;
using JobManagement.Domain.AggregatesModel.JobAggregate;

namespace JobManagement.Application.IntegrationEvents.Handlers
{
    public class CreditsLimitExceededIntegrationEventHandler : IIntegrationEventHandler<CreditsLimitExceededIntegrationEvent>
    {
        private readonly IJobRepository _jobRepository;

        public CreditsLimitExceededIntegrationEventHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task HandleAsync(CreditsLimitExceededIntegrationEvent @event)
        {
            var job = await _jobRepository.GetByIdAsync(@event.JobId);

            job.RemoveProposal(@event.ProposalId);

            await _jobRepository.UnitOfWork.SaveChangesAsync();
        }

    }
}
