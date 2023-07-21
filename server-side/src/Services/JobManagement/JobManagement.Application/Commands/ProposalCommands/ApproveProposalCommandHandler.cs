using FluentResults;
using JobManagement.Application.IntegrationEvents;
using JobManagement.Application.Notifications;
using JobManagement.Domain.Repositories;
using MediatR;

namespace JobManagement.Application.Commands.ProposalCommands
{
    public class ApproveProposalCommandHandler : IRequestHandler<ApproveProposalCommand, Result>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IJobIntegrationEventService _integrationEventService;

        public ApproveProposalCommandHandler(
            IJobRepository jobRepository,
            IJobIntegrationEventService integrationEventService)
        {
            _jobRepository = jobRepository;
            _integrationEventService = integrationEventService;
        }

        public async Task<Result> Handle(ApproveProposalCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                return Result.Fail("Job does not exist");

            job.SetProposalStatusToClientApproved(request.ProposalId);

            await _jobRepository.UnitOfWork.SaveEntitiesAsync();

            var proposal = job.GetProposal(request.ProposalId);
            var notification = new ProposalApprovedNotification(job.Id, job.Title, proposal.Id, proposal.FreelancerId);
            await _integrationEventService.SaveEventAsync(notification);

            return Result.Ok();
        }
    }
}
