using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class EducationUpdatedDomainEventHandler : INotificationHandler<EducationUpdatedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public EducationUpdatedDomainEventHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(EducationUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var education = _mapper.Map<EducationViewModel>(notification.Education);
            await _repository.UpdateNestedListItemAsync(notification.FreelancerId, fr => fr.Educations, education.Id, education);
        }
    }
}
