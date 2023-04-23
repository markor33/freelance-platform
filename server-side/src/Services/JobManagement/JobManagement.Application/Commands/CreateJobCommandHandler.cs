using FluentResults;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using MediatR;

namespace JobManagement.Application.Commands
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Result<Job>>
    {
        private readonly IJobRepository _jobRepository;

        public CreateJobCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<Result<Job>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var job = new Job(request.ClientId, request.Title, request.Description, request.ExperienceLevel, request.Payment);

            job = await _jobRepository.CreateAsync(job);
            foreach(var question in request.Questions)
                job.AddQuestion(question);

            var result = await _jobRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!result)
                return Result.Fail("Job creation failed");
            return Result.Ok(job);
        }
    }
}
