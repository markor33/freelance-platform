using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class EmploymentupdatedDomainEventHandler : INotificationHandler<EmploymentUpdatedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public EmploymentupdatedDomainEventHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(EmploymentUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var freelancer = await _repository.GetByIdAsync(notification.FreelancerId);
            freelancer.UpdateEmployment(_mapper.Map<EmploymentViewModel>(notification.Employment));

            await _repository.UpdateAsync(freelancer);
        }
    }
}
