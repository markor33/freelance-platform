using FluentResults;
using JobManagement.Application.IntegrationEvents;
using JobManagement.Application.IntegrationEvents.Events;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.Repositories;
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

            var proposal = Proposal.Create(request.FreelancerId, request.Text, request.Payment, request.Answers);

            var addProposalResult = job.AddProposal(proposal);
            if (addProposalResult.IsFailed)
                return addProposalResult;

            var result = await _jobRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!result)
                return Result.Fail("Proposal creation failed");

            var eventMessage = new ProposalCreatedIntegrationEvent(request.FreelancerId, job.Id, proposal.Id, job.Credits);
            await _integrationEventService.SaveEventAsync(eventMessage);

            return Result.Ok(proposal);
        }

    }
}
