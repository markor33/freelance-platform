using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class EmploymentupdatedDomainEventHandler : INotificationHandler<EmploymentUpdated>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public EmploymentupdatedDomainEventHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(EmploymentUpdated notification, CancellationToken cancellationToken)
        {
            var employment = new EmploymentViewModel(notification.EmploymentId, notification.Company, 
                notification.Title, notification.Period, notification.Description);
            await _repository.UpdateNestedListItemAsync(notification.AggregateId, fr => fr.Employments, employment.Id, employment);
        }
    }
}
