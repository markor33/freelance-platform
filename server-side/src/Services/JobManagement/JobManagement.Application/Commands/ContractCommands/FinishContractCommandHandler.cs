using EventBus.Abstractions;
using FluentResults;
using JobManagement.Application.IntegrationEvents.Events;
using JobManagement.Application.Notifications;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using MediatR;

namespace JobManagement.Application.Commands.ContractCommands
{
    public class FinishContractCommandHandler : IRequestHandler<FinishContractCommand, Result>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IEventBus _eventBus;

        public FinishContractCommandHandler(IJobRepository jobRepository, IEventBus eventBus)
        {
            _jobRepository = jobRepository;
            _eventBus = eventBus;
        }

        public async Task<Result> Handle(FinishContractCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                return Result.Fail("Job does not exist");

            var result = job.ChangeContractStatus(request.ContractId, ContractStatus.FINISHED);
            if (result.IsFailed)
                return Result.Fail("");

            await _jobRepository.UnitOfWork.SaveEntitiesAsync();

            var notification = new ContractFinishedNotification(request.ContractId, result.Value.FreelancerId, request.JobId, job.Title);
            _eventBus.Publish(notification);
            var integrationEvent = new ContractFinishedIntegrationEvent(request.ContractId, job.Id, job.ClientId, result.Value.FreelancerId);
            _eventBus.Publish(integrationEvent);

            return Result.Ok();
        }
    }
}
