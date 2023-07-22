using FluentResults;
using JobManagement.Application.IntegrationEvents;
using JobManagement.Application.IntegrationEvents.Events;
using JobManagement.Application.Notifications;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.Repositories;
using MediatR;

namespace JobManagement.Application.Commands.ContractCommands
{
    public class FinishContractCommandHandler : IRequestHandler<FinishContractCommand, Result>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IJobIntegrationEventService _integrationEventService;

        public FinishContractCommandHandler(
            IJobRepository jobRepository,
            IJobIntegrationEventService integrationEventService)
        {
            _jobRepository = jobRepository;
            _integrationEventService = integrationEventService;
        }

        public async Task<Result> Handle(FinishContractCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                return Result.Fail("Job does not exist");

            var contract = job.FinishContract(request.ContractId);

            await _jobRepository.UnitOfWork.SaveEntitiesAsync();

            var notification = new ContractFinishedNotification(request.ContractId, contract.FreelancerId, request.JobId, job.Title);
            await _integrationEventService.SaveEventAsync(notification);
            var integrationEvent = new ContractFinishedIntegrationEvent(request.ContractId, job.Id, job.ClientId, contract.FreelancerId);
            await _integrationEventService.SaveEventAsync(integrationEvent);

            return Result.Ok();
        }
    }
}
