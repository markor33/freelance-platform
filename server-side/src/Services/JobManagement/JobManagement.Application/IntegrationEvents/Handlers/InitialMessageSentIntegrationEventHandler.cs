using EventBus.Abstractions;
using JobManagement.Application.IntegrationEvents.Events;
using JobManagement.Application.Notifications;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;

namespace JobManagement.Application.IntegrationEvents.Handlers
{
    public class InitialMessageSentIntegrationEventHandler : IIntegrationEventHandler<InitialMessageSentIntegrationEvent>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IEventBus _eventBus;

        public InitialMessageSentIntegrationEventHandler(IJobRepository jobRepository, IEventBus eventBus)
        {
            _jobRepository = jobRepository;
            _eventBus = eventBus;
        }

        public async Task HandleAsync(InitialMessageSentIntegrationEvent @event)
        {
            var job = await _jobRepository.GetByIdAsync(@event.JobId);

            var proposal = job.GetProposal(@event.ProposalId);
            if (proposal is null)
                return; 
            proposal.ChangeStatus(ProposalStatus.INTERVIEW);

            await _jobRepository.UnitOfWork.SaveEntitiesAsync();

            var interviewStageStartedNotification = new InterviewStageStartedNotification(@event.FreelancerId, job.Id, job.Title, @event.ProposalId);
            _eventBus.Publish(interviewStageStartedNotification);
        }
    }

}
