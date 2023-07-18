﻿using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class CertificationAddedDomainEventHandler : INotificationHandler<CertificationAddedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public CertificationAddedDomainEventHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CertificationAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            var certification = _mapper.Map<CertificationViewModel>(notification.Certification);
            await _repository.AddToNestedListAsync(notification.FreelancerId, fr => fr.Certifications, certification);
        }
    }
}
