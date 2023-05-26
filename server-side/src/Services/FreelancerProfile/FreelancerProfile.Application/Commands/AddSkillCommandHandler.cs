using FluentResults;
using FreelancerProfile.Application.Services;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class AddSkillCommandHandler : IRequestHandler<AddSkillCommand, Result<List<Skill>>>
    {
        private readonly IFreelancerRepository _freelancerRepository;
        private readonly ISkillService _skillService;

        public AddSkillCommandHandler(
            IFreelancerRepository freelancerRepository,
            ISkillService skillService)
        {
            _freelancerRepository = freelancerRepository;
            _skillService = skillService;
        }

        public async Task<Result<List<Skill>>> Handle(AddSkillCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByUserIdAsync(request.UserId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            var skills = new List<Skill>();
            foreach(var skillId in request.Skills)
            {
                var skill = await _skillService.GetByIdAsync(skillId);
                if (skill is null) 
                    return Result.Fail($"Skill does not exist");

                skills.Add(skill);
            }
            freelancer.UpdateSkills(skills);

            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!result)
                return Result.Fail("Skills assigning failed");

            return Result.Ok(freelancer.Skills);
        }
    }
}
