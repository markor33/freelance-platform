using EventBus.Abstractions;
using FluentResults;
using JobManagement.Application.Notifications;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using MediatR;

namespace JobManagement.Application.Commands
{
    public class FreelancerAcceptProposalCommandHandler : IRequestHandler<FreelancerAcceptProposalCommand, Result>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IEventBus _eventBus;

        public FreelancerAcceptProposalCommandHandler(
            IJobRepository jobRepository,
            IEventBus eventBus)
        {
            _jobRepository = jobRepository;
            _eventBus = eventBus;
        }

        public async Task<Result> Handle(FreelancerAcceptProposalCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                return Result.Fail("Job does not exist");

            job.ChangeProposalStatus(request.ProposalId, ProposalStatus.ACCEPTED);

            await _jobRepository.UnitOfWork.SaveEntitiesAsync();

            var proposal = job.GetProposal(request.ProposalId);
            var notification = new FreelancerAcceptedProposalNotification(job.Id, job.Title, proposal.Id, job.ClientId);
            _eventBus.Publish(notification);

            return Result.Ok();
        }
    }
}
