using FluentResults;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using MediatR;

namespace JobManagement.Application.Commands.ContractCommands
{
    public class TerminateContractCommandHandler : IRequestHandler<TerminateContractCommand, Result>
    {
        private readonly IJobRepository _jobRepository;

        public TerminateContractCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<Result> Handle(TerminateContractCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                return Result.Fail("Job does not exist");

            job.ChangeContractStatus(request.ContractId, ContractStatus.TERMINATED);

            await _jobRepository.UnitOfWork.SaveEntitiesAsync();

            return Result.Ok();
        }
    }
}
