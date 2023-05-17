using EventBus.Abstractions;
using JobManagement.Application.IntegrationEvents.Events;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;

namespace JobManagement.Application.IntegrationEvents.Handlers
{
    public class InitialMessageSentIntegrationEventHandler : IIntegrationEventHandler<InitialMessageSentIntegrationEvent>
    {
        private readonly IJobRepository _jobRepository;

        public InitialMessageSentIntegrationEventHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task HandleAsync(InitialMessageSentIntegrationEvent @event)
        {
            var job = await _jobRepository.GetByIdAsync(@event.JobId);
            job.ChangeProposalStatus(@event.ProposalId, ProposalStatus.INTERVIEW);

            await _jobRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }

}
