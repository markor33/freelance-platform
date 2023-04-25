using FluentResults;
using JobManagement.Application.Services;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using MediatR;

namespace JobManagement.Application.Commands
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Result<Job>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IProfessionService _professionService;
        private readonly ISkillService _skillService;

        public CreateJobCommandHandler(
            IJobRepository jobRepository, 
            IProfessionService professionService,
            ISkillService skillService)
        {
            _jobRepository = jobRepository;
            _professionService = professionService;
            _skillService = skillService;
        }

        public async Task<Result<Job>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var profession = await _professionService.GetByIdAsync(request.ProfessionId);
            if (profession is null)
                return Result.Fail($"Profession with '{request.ProfessionId}' id, does not exist");

            var job = new Job(request.ClientId, request.Title, request.Description, request.ExperienceLevel, request.Payment, profession);

            job.AddQuestions(request.Questions);
            await AddSkills(job, request.Skills);

            job = await _jobRepository.CreateAsync(job);
            var result = await _jobRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!result)
                return Result.Fail("Job creation failed");
            return Result.Ok(job);
        }

        private async Task<Result> AddSkills(Job job, List<Guid> skills)
        {
            foreach (var skillId in skills)
            {
                var skill = await _skillService.GetByIdAsync(skillId);
                if (skill is null)
                    return Result.Fail($"Skill does not exist");

                var addSkillResult = job.AddSkill(skill);
                if (addSkillResult.IsFailed)
                    return addSkillResult;
            }
            return Result.Ok();
        }

    }
}
