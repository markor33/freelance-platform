using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class CertificationDeletedDomainEventHandler : INotificationHandler<CertificationDeleted>
    {
        private readonly IFreelancerReadModelRepository _repository;

        public CertificationDeletedDomainEventHandler(IFreelancerReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CertificationDeleted notification, CancellationToken cancellationToken)
        {
            await _repository.RemoveFromNestedListAsync(notification.AggregateId, fr => fr.Certifications, notification.CertificationId);
        }
    }
}
