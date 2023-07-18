using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class EmploymentDeletedDomainEventHandler : INotificationHandler<EmploymentDeletedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;

        public EmploymentDeletedDomainEventHandler(IFreelancerReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(EmploymentDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _repository.RemoveFromNestedListAsync(notification.FreelancerId, fr => fr.Employments, notification.EmploymentId);
        }
    }
}
