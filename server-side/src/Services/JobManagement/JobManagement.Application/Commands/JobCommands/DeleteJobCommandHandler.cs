using FluentResults;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using MediatR;

namespace JobManagement.Application.Commands.JobCommands
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, Result>
    {
        private readonly IJobRepository _repository;

        public DeleteJobCommandHandler(IJobRepository repository)
        {
            _repository = repository;
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

            return Result.Ok();
        }
    }
}
