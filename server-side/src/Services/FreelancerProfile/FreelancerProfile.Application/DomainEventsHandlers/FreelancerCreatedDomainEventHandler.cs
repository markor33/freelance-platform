using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class FreelancerCreatedDomainEventHandler : INotificationHandler<FreelancerCreatedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public FreelancerCreatedDomainEventHandler(
            IFreelancerReadModelRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(FreelancerCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var freelancerViewModel = _mapper.Map<FreelancerViewModel>(notification.Freelancer);
            await _repository.CreateAsync(freelancerViewModel);
        }
    }
}
