using FluentResults;
using FreelancerProfile.Application.Services;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.Repositories;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class AddSkillCommandHandler : IRequestHandler<AddSkillCommand, Result<List<Skill>>>
    {
        private readonly IFreelancerRepository _freelancerRepository;
        private readonly ISkillRepository _skillRepository;

        public AddSkillCommandHandler(
            IFreelancerRepository freelancerRepository,
            ISkillRepository skillRepository)
        {
            _freelancerRepository = freelancerRepository;
            _skillRepository = skillRepository;
        }

        public async Task<Result<List<Skill>>> Handle(AddSkillCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(request.FreelancerId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            var skillsToRemove = freelancer.Skills.Where(s => !request.Skills.Any(ns => ns == s.Id)).ToList();
            freelancer.RemoveSkills(skillsToRemove);

            var skillsToAdd = request.Skills.Where(ns => !freelancer.Skills.Any(s => s.Id == ns)).ToList();

            var skills = await _skillRepository.GetByIdsAsync(skillsToAdd);
            /*foreach(var skillId in skillsToAdd)
            {
                var skill = await _skillService.GetByIdAsync(skillId);
                if (skill is null) 
                    return Result.Fail($"Skill does not exist");

                skills.Add(skill);
            }*/
            freelancer.AddSkills(skills);

            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!result)
                return Result.Fail("Skills assigning failed");

            return Result.Ok(freelancer.Skills);
        }
    }
}
