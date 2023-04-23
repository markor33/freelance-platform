using EventBus.Abstractions;
using JobManagement.Application.IntegrationEvents.Events;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;

namespace JobManagement.Application.IntegrationEvents.Handlers
{
    public class CreditsReservedIntegrationEventHandler : IIntegrationEventHandler<CreditsReservedIntegrationEvent>
    {
        private readonly IJobRepository _jobRepository;

        public CreditsReservedIntegrationEventHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task HandleAsync(CreditsReservedIntegrationEvent @event)
        {
            var job = await _jobRepository.GetByIdAsync(@event.JobId);

            var proposal = job.Proposals.First(j => j.Id == @event.ProposalId);
            proposal.ChangeStatus(ProposalStatus.SENT);

            await _jobRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
