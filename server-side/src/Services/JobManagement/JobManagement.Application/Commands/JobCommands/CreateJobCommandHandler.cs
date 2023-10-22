using FluentResults;
using JobManagement.Application.IntegrationEvents;
using JobManagement.Application.IntegrationEvents.Events;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.Repositories;
using MediatR;

namespace JobManagement.Application.Commands.JobCommands
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Result<Job>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IProfessionRepository _professionRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IJobIntegrationEventService _integrationEventService;

        public CreateJobCommandHandler(
            IJobRepository jobRepository,
            IProfessionRepository professionRepository,
            ISkillRepository skillRepository,
            IJobIntegrationEventService integrationEventService)
        {
            _jobRepository = jobRepository;
            _professionRepository = professionRepository;
            _skillRepository = skillRepository;
            _integrationEventService = integrationEventService;
        }

        public async Task<Result<Job>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var profession = await _professionRepository.GetByIdAsync(request.ProfessionId);
            if (profession is null)
                return Result.Fail($"Profession with '{request.ProfessionId}' id, does not exist");

            var skills = await _skillRepository.GetByIdsAsync(request.Skills);

            var job = Job.Create(request.ClientId, request.Title, request.Description, request.ExperienceLevel, 
                request.Payment, profession, request.Questions, skills);

            job = await _jobRepository.CreateAsync(job);
            var result = await _jobRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!result)
                return Result.Fail("Job creation failed");

            var eventMessage = new JobCreatedIntegrationEvent(job);
            await _integrationEventService.SaveEventAsync(eventMessage);

            return Result.Ok(job);
        }

    }
}
