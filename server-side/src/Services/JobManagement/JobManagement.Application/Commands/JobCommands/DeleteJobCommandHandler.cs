using FluentResults;
using JobManagement.Application.IntegrationEvents;
using JobManagement.Application.IntegrationEvents.Events;
using JobManagement.Domain.Repositories;
using MediatR;

namespace JobManagement.Application.Commands.JobCommands
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, Result>
    {
        private readonly IJobRepository _repository;
        private readonly IJobIntegrationEventService _integrationEventService;

        public DeleteJobCommandHandler(
            IJobRepository repository,
            IJobIntegrationEventService integrationEventService)
        {
            _repository = repository;
            _integrationEventService = integrationEventService;
        }

        public async Task<Result> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _repository.GetByIdAsync(request.JobId);
            if (job is null)
                return Result.Fail("Job does not exist");

            var deleteResult = job.Delete();
            if (deleteResult.IsFailed)
                return deleteResult;

            var result = await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!result)
                return Result.Fail("");

            var eventMessage = new JobDeletedIntegrationEvent(job.Id);
            await _integrationEventService.SaveEventAsync(eventMessage);

            return Result.Ok();
        }
    }
}
