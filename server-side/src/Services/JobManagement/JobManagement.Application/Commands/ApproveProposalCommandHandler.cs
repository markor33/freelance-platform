using EventBus.Abstractions;
using FluentResults;
using JobManagement.Application.Notifications;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using MediatR;

namespace JobManagement.Application.Commands
{
    public class ApproveProposalCommandHandler : IRequestHandler<ApproveProposalCommand, Result>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IEventBus _eventBus;

        public ApproveProposalCommandHandler(
            IJobRepository jobRepository,
            IEventBus eventBus)
        {
            _jobRepository = jobRepository;
            _eventBus = eventBus;
        }

        public async Task<Result> Handle(ApproveProposalCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                return Result.Fail("Job does not exist");

            job.ChangeProposalStatus(request.ProposalId, ProposalStatus.CLIENT_APPROVED);

            await _jobRepository.UnitOfWork.SaveEntitiesAsync();

            var proposal = job.GetProposal(request.ProposalId);
            var notification = new ClientAcceptedProposalNotification(job.Id, job.Title, proposal.Id, proposal.FreelancerId);
            _eventBus.Publish(notification);

            return Result.Ok();
        }
    }
}
