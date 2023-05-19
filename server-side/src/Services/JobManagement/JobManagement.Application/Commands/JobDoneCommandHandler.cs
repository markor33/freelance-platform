using EventBus.Abstractions;
using FluentResults;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using MediatR;

namespace JobManagement.Application.Commands
{
    public class JobDoneCommandHandler : IRequestHandler<JobDoneCommand, Result>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IEventBus _eventBus;

        public JobDoneCommandHandler(
            IJobRepository jobRepository,
            IEventBus eventBus)
        {
            _jobRepository = jobRepository;
            _eventBus = eventBus;
        }

        public async Task<Result> Handle(JobDoneCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                return Result.Fail("Job does not exist");

            job.Done();
            await _jobRepository.UnitOfWork.SaveEntitiesAsync();

            return Result.Ok();
        }
    }
}
