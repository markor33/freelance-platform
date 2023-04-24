using FluentResults;
using FreelancerProfile.Application.Services;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class AddSkillCommandHandler : IRequestHandler<AddSkillCommand, Result>
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

        public async Task<Result> Handle(AddSkillCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByUserIdAsync(request.UserId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            foreach(var skillId in request.Skills)
            {
                var skill = await _skillService.GetByIdAsync(skillId);
                if (skill is null) 
                    return Result.Fail($"Skill does not exist");

                var addSkillResult = freelancer.AddSkill(skill);
                if (addSkillResult.IsFailed)
                    return addSkillResult;
            }

            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!result)
                return Result.Fail("Skills assigning failed");
            return Result.Ok();
        }
    }
}
