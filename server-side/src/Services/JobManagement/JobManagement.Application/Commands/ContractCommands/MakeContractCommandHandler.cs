using FluentResults;
using JobManagement.Application.IntegrationEvents;
using JobManagement.Application.Notifications;
using JobManagement.Domain.Repositories;
using MediatR;

namespace JobManagement.Application.Commands.ContractCommands
{
    public class MakeContractCommandHandler : IRequestHandler<MakeContractCommand, Result>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IJobIntegrationEventService _integrationEventService;

        public MakeContractCommandHandler(
            IJobRepository jobRepository,
            IJobIntegrationEventService integrationEventService)
        {
            _jobRepository = jobRepository;
            _integrationEventService = integrationEventService;
        }

        public async Task<Result> Handle(MakeContractCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                return Result.Fail("Job does not exist");

            var contract = job.MakeContract(request.ProposalId).Value;

            await _jobRepository.UnitOfWork.SaveEntitiesAsync();

            var notification = new ContractMadeNotification(contract.Id, job.Id, job.Title, request.ProposalId, job.ClientId);
            await _integrationEventService.SaveEventAsync(notification);

            return Result.Ok();
        }
    }
}
