using EventBus.Abstractions;
using FluentResults;
using JobManagement.Application.Notifications;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using MediatR;

namespace JobManagement.Application.Commands
{
    public class ClientAcceptProposalCommandHandler : IRequestHandler<ClientAcceptProposalCommand, Result>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IEventBus _eventBus;

        public ClientAcceptProposalCommandHandler(
            IJobRepository jobRepository,
            IEventBus eventBus)
        {
            _jobRepository = jobRepository;
            _eventBus = eventBus;
        }

        public async Task<Result> Handle(ClientAcceptProposalCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                return Result.Fail("Job does not exist");

            var proposal = job.GetProposal(request.ProposalId);
            if (proposal is null)
                return Result.Fail("Proposal does not exist");
            proposal.ChangeStatus(ProposalStatus.CLIENT_ACCEPTED);

            await _jobRepository.UnitOfWork.SaveEntitiesAsync();

            var notification = new ClientAcceptedProposalNotification(job.Id, job.Title, proposal.Id, proposal.FreelancerId);
            _eventBus.Publish(notification);

            return Result.Ok();
        }
    }
}
