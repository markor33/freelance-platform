using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class SkillsUpdatedDomainEventeHandler : INotificationHandler<SkillsUpdated>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public SkillsUpdatedDomainEventeHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(SkillsUpdated notification, CancellationToken cancellationToken)
        {
            var skills = _mapper.Map<List<SkillViewModel>>(notification.Skills);
            await _repository.UpdateAsync(notification.AggregateId, fr => fr.Skills, skills);
        }
    }
}
