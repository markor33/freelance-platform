using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class EmploymentAddedDomainEventHandler : INotificationHandler<EmploymentAddedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public EmploymentAddedDomainEventHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(EmploymentAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            var freelancer = await _repository.GetByIdAsync(notification.FreelancerId);
            freelancer.AddEmployment(_mapper.Map<EmploymentViewModel>(notification.Employment));

            await _repository.UpdateAsync(freelancer);
        }
    }
}
