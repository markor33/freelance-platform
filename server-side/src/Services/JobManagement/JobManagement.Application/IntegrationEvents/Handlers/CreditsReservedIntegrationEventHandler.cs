using EventBus.Abstractions;
using JobManagement.Application.IntegrationEvents.Events;
using JobManagement.Application.Notifications;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;

namespace JobManagement.Application.IntegrationEvents.Handlers
{
    public class CreditsReservedIntegrationEventHandler : IIntegrationEventHandler<CreditsReservedIntegrationEvent>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IEventBus _eventBus;

        public CreditsReservedIntegrationEventHandler(
            IJobRepository jobRepository, 
            IEventBus eventBus)
        {
            _jobRepository = jobRepository;
            _eventBus = eventBus;
        }

        public async Task HandleAsync(CreditsReservedIntegrationEvent @event)
        {
            var job = await _jobRepository.GetByIdAsync(@event.JobId);

            var proposal = job.Proposals.First(j => j.Id == @event.ProposalId);
            proposal.ChangeStatus(ProposalStatus.SENT);

            await _jobRepository.UnitOfWork.SaveChangesAsync();

            var proposalSubmittedNotification = new ProposalSubmittedNotification(job.ClientId, job.Id, job.Title);
            _eventBus.Publish(proposalSubmittedNotification);
        }
    }
}
