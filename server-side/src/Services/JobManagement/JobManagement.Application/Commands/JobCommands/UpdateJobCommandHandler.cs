using FluentResults;
using JobManagement.Application.Services;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using MediatR;

namespace JobManagement.Application.Commands.JobCommands
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, Result<Job>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IProfessionService _professionService;
        private readonly ISkillService _skillService;

        public UpdateJobCommandHandler(
            IJobRepository jobRepository,
            IProfessionService professionService,
            ISkillService skillService)
        {
            _jobRepository = jobRepository;
            _professionService = professionService;
            _skillService = skillService;
        }

        public async Task<Result<Job>> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job is null)
                return Result.Fail("Job does not exist");

            var profession = await _professionService.GetByIdAsync(request.ProfessionId);
            if (profession is null)
                return Result.Fail($"Profession with '{request.ProfessionId}' id, does not exist");

            var skills = await GetSkills(request.Skills);

            job.Update(request.Title, request.Description, request.ExperienceLevel, request.Payment, profession, request.Questions, skills);

            var result = await _jobRepository.UnitOfWork.SaveEntitiesAsync();
            if (!result)
                return Result.Fail("job update failed");

            return Result.Ok(job);
        }

        private async Task<List<Skill>> GetSkills(List<Guid> skillIds)
        {
            var skills = new List<Skill>();
            foreach (var skillId in skillIds)
            {
                var skill = await _skillService.GetByIdAsync(skillId);
                if (skill is null)
                    continue;

                skills.Add(skill);
            }
            return skills;
        }

    }
}
