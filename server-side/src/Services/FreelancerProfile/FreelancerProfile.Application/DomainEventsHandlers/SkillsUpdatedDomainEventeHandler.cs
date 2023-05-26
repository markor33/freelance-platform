using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class SkillsUpdatedDomainEventeHandler : INotificationHandler<SkillsUpdatedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public SkillsUpdatedDomainEventeHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(SkillsUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var freelancer = await _repository.GetByIdAsync(notification.FreelancerId);

            freelancer.SetSkills(notification.Skills.Select(s => _mapper.Map<SkillViewModel>(s)).ToList());

            await _repository.UpdateAsync(freelancer);
        }
    }
}
