using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class CertificationUpdatedDomainEventHandler : INotificationHandler<CertificationUpdatedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public CertificationUpdatedDomainEventHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CertificationUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var certification = _mapper.Map<CertificationViewModel>(notification.Certification);
            await _repository.UpdateNestedListItemAsync(notification.FreelancerId, fr => fr.Certifications, certification.Id, certification);
        }
    }
}
