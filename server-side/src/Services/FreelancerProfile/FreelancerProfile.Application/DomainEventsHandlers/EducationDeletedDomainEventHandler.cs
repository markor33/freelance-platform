using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class EducationDeletedDomainEventHandler : INotificationHandler<EducationDeleted>
    {
        private readonly IFreelancerReadModelRepository _repository;

        public EducationDeletedDomainEventHandler(IFreelancerReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(EducationDeleted notification, CancellationToken cancellationToken)
        {
            await _repository.RemoveFromNestedListAsync(notification.AggregateId, fr => fr.Educations, notification.EducationId);
        }
    }
}
