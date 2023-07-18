using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class EducationAddedDomainEventHandler : INotificationHandler<EducationAddedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public EducationAddedDomainEventHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(EducationAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            var education = _mapper.Map<EducationViewModel>(notification.Education);
            await _repository.AddToNestedListAsync(notification.FreelancerId, fr => fr.Educations, education);
        }
    }
}
