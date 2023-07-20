using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class EmploymentDeletedDomainEventHandler : INotificationHandler<EmploymentDeleted>
    {
        private readonly IFreelancerReadModelRepository _repository;

        public EmploymentDeletedDomainEventHandler(IFreelancerReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(EmploymentDeleted notification, CancellationToken cancellationToken)
        {
            await _repository.RemoveFromNestedListAsync(notification.AggregateId, fr => fr.Employments, notification.EmploymentId);
        }
    }
}
