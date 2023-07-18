using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class EducationDeletedDomainEventHandler : INotificationHandler<EducationDeletedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;

        public EducationDeletedDomainEventHandler(IFreelancerReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(EducationDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _repository.RemoveFromNestedListAsync(notification.FreelancerId, fr => fr.Educations, notification.EducationId);
        }
    }
}
