using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class EmploymentAddedDomainEventHandler : INotificationHandler<EmploymentAddedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public EmploymentAddedDomainEventHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(EmploymentAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            var employment = _mapper.Map<EmploymentViewModel>(notification.Employment);
            await _repository.AddToNestedListAsync(notification.FreelancerId, fr => fr.Employments, employment);
        }
    }
}
