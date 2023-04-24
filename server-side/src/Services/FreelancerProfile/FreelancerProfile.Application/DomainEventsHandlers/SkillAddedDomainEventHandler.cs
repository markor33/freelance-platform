using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class SkillAddedDomainEventHandler : INotificationHandler<SkillAddedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public SkillAddedDomainEventHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(SkillAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            var freelancer = await _repository.GetByIdAsync(notification.FreelancerId);
            freelancer.AddSkill(_mapper.Map<SkillViewModel>(notification.Skill));

            await _repository.UpdateAsync(freelancer);
        }
    }
}
