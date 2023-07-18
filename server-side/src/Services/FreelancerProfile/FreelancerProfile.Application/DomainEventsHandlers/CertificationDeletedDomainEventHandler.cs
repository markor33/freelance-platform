using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class CertificationDeletedDomainEventHandler : INotificationHandler<CertificationDeletedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;

        public CertificationDeletedDomainEventHandler(IFreelancerReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CertificationDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _repository.RemoveFromNestedListAsync(notification.FreelancerId, fr => fr.Certifications, notification.Certificationid);
        }
    }
}
