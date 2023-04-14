using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class AddSkillCommandHandler : IRequestHandler<AddSkillCommand, bool>
    {
        private readonly IFreelancerRepository _freelancerRepository;
        private readonly ISkillQueries _skillQueries;

        public AddSkillCommandHandler(
            IFreelancerRepository freelancerRepository,
            ISkillQueries skillQueries)
        {
            _freelancerRepository = freelancerRepository;
            _skillQueries = skillQueries;
        }

        public async Task<bool> Handle(AddSkillCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByUserIdAsync(request.UserId);
            if (freelancer is null)
                return false;

            foreach(var skillId in request.Skills)
            {
                var skill = await _skillQueries.GetByIdAsync(skillId);
                if (skill is null) return false;
                freelancer.AddSkill(skill);
            }

            var result = await _freelancerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            if (result == 0)
                return false;
            return true;
        }
    }
}
