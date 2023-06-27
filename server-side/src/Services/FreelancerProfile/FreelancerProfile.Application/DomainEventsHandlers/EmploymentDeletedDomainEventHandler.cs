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
            var freelancer = await _repository.GetByIdAsync(notification.FreelancerId);
            freelancer.DeleteEmployment(notification.EmploymentId);

            await _repository.UpdateAsync(freelancer);
        }
    }
}
