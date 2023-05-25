using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class CertificationEdittedDomainEventHandler : INotificationHandler<CertificationEdittedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public CertificationEdittedDomainEventHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CertificationEdittedDomainEvent notification, CancellationToken cancellationToken)
        {
            var freelancer = await _repository.GetByIdAsync(notification.FreelancerId);

            freelancer.UpdateCertification(_mapper.Map<CertificationViewModel>(notification.Certification));

            await _repository.UpdateAsync(freelancer);
        }
    }
}
