using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class CertificationUpdatedDomainEventHandler : INotificationHandler<CertificationUpdated>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public CertificationUpdatedDomainEventHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CertificationUpdated notification, CancellationToken cancellationToken)
        {
            // var certification = _mapper.Map<CertificationViewModel>(notification.CertificationId);
            var certification = new CertificationViewModel(notification.CertificationId, notification.Name, 
                notification.Provider, notification.Attended, notification.Description);
            await _repository.UpdateNestedListItemAsync(notification.AggregateId, fr => fr.Certifications, certification.Id, certification);
        }
    }
}
