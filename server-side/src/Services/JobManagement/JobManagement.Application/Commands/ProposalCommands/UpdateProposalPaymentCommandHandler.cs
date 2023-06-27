using FluentResults;
using JobManagement.Application.IntegrationEvents;
using JobManagement.Application.Notifications;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using MediatR;

namespace JobManagement.Application.Commands.ProposalCommands
{
    public class UpdateProposalPaymentCommandHandler : IRequestHandler<UpdateProposalPaymentCommand, Result>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IJobIntegrationEventService _integrationEventService;

        public UpdateProposalPaymentCommandHandler(
            IJobRepository jobRepository,
            IJobIntegrationEventService integrationEventService)
        {
            _jobRepository = jobRepository;
            _integrationEventService = integrationEventService;
        }

        public async Task<Result> Handle(UpdateProposalPaymentCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                return Result.Fail("Job does not exist");

            var proposal = job.GetProposal(request.ProposalId);
            if (proposal is null)
                return Result.Fail("Proposal does not exist");
            proposal.ChangePayment(request.Payment);

            await _jobRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            var notification = new ProposalPaymentChangedNotification(job.Id, job.Title, proposal.Id, proposal.FreelancerId);
            await _integrationEventService.SaveEventAsync(notification);

            return Result.Ok();
        }
    }
}
