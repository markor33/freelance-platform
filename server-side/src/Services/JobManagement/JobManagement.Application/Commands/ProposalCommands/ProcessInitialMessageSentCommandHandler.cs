using JobManagement.Application.IntegrationEvents;
using JobManagement.Application.Notifications;
using JobManagement.Domain.Repositories;
using MediatR;

namespace JobManagement.Application.Commands.ProposalCommands
{
    public class ProcessInitialMessageSentCommandHandler : IRequestHandler<ProcessInitialMessageSentCommand, Unit>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IJobIntegrationEventService _integrationEventService;

        public ProcessInitialMessageSentCommandHandler(IJobRepository jobRepository, IJobIntegrationEventService integrationEventService)
        {
            _jobRepository = jobRepository;
            _integrationEventService = integrationEventService;
        }

        public async Task<Unit> Handle(ProcessInitialMessageSentCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);

            job.SetProposalStatusToInterview(request.ProposalId);

            await _jobRepository.UnitOfWork.SaveEntitiesAsync();

            var notification = new InterviewStageStartedNotification(request.FreelancerId, job.Id, job.Title, request.ProposalId);
            await _integrationEventService.SaveEventAsync(notification);

            return Unit.Value;
        }
    }
}
