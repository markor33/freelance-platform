using FluentResults;
using JobManagement.Application.IntegrationEvents;
using JobManagement.Application.IntegrationEvents.Events;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using MediatR;

namespace JobManagement.Application.Commands.ProposalCommands
{
    public class CreateProposalCommandHandler : IRequestHandler<CreateProposalCommand, Result<Proposal>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IJobIntegrationEventService _integrationEventService;

        public CreateProposalCommandHandler(
            IJobRepository jobRepository, 
            IJobIntegrationEventService integrationEventService)
        {
            _jobRepository = jobRepository;
            _integrationEventService = integrationEventService;
        }

        public async Task<Result<Proposal>> Handle(CreateProposalCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                return Result.Fail("Job does not exist");

            var proposal = new Proposal(request.FreelancerId, request.Text, request.Payment);
            foreach (var answer in request.Answers)
                proposal.AddAnswer(answer);
            var addProposalResult = job.AddProposal(proposal);
            if (addProposalResult.IsFailed)
                return addProposalResult;

            var result = await _jobRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (result == 0)
                return Result.Fail("Proposal creation failed");

            var eventMessage = new ProposalCreatedIntegrationEvent(request.FreelancerId, job.Id, proposal.Id, job.Credits);
            await _integrationEventService.SaveEventAsync(eventMessage);

            return Result.Ok(proposal);
        }

    }
}
