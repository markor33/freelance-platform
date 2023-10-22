using FluentResults;
using JobManagement.Application.IntegrationEvents;
using JobManagement.Application.IntegrationEvents.Events;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.Repositories;
using MediatR;

namespace JobManagement.Application.Commands.JobCommands
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, Result<Job>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IJobIntegrationEventService _integrationEventService;

        public UpdateJobCommandHandler(
            IJobRepository jobRepository,
            ISkillRepository skillRepository,
            IJobIntegrationEventService integrationEventService)
        {
            _jobRepository = jobRepository;
            _skillRepository = skillRepository;
            _integrationEventService = integrationEventService;
        }

        public async Task<Result<Job>> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                return Result.Fail("Job does not exist");

            var skillsToRemove = job.Skills.Where(s => !request.Skills.Any(ns => ns == s.Id)).ToList();
            job.RemoveSkills(skillsToRemove);

            var skillsToAdd = request.Skills.Where(ns => !job.Skills.Any(s => s.Id == ns)).ToList();
            var skills = await _skillRepository.GetByIdsAsync(skillsToAdd);
            job.AddSkills(skills);

            job.Update(request.Title, request.Description, request.ExperienceLevel, request.Payment, request.ProfessionId, request.Questions, skills);

            var result = await _jobRepository.UnitOfWork.SaveEntitiesAsync();
            if (!result)
                return Result.Fail("job update failed");

            var eventMessage = new JobUpdatedIntegrationEvent(job);
            await _integrationEventService.SaveEventAsync(eventMessage);

            return Result.Ok(job);
        }

    }
}
