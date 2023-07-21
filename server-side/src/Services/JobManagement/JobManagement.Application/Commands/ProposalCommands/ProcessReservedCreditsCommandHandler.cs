using JobManagement.Application.IntegrationEvents;
using JobManagement.Application.Notifications;
using JobManagement.Domain.Repositories;
using MediatR;

namespace JobManagement.Application.Commands.ProposalCommands
{
    public class ProcessReservedCreditsCommandHandler : IRequestHandler<ProcessReservedCreditsCommand, Unit>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IJobIntegrationEventService _integrationEventService;

        public ProcessReservedCreditsCommandHandler(
            IJobRepository jobRepository,
            IJobIntegrationEventService integrationEventService)
        {
            _jobRepository = jobRepository;
            _integrationEventService = integrationEventService;
        }

        public async Task<Unit> Handle(ProcessReservedCreditsCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);

            job.SetProposalStatusToSent(request.ProposalId);

            await _jobRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            var proposalSubmittedNotification = new ProposalSubmittedNotification(job.ClientId, job.Id, job.Title);
            await _integrationEventService.SaveEventAsync(proposalSubmittedNotification);

            return Unit.Value;
        }
    }
}
