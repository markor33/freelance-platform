using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class ProfileSetupCompletedDomainEventHandler : INotificationHandler<ProfileSetupCompletedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public ProfileSetupCompletedDomainEventHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(ProfileSetupCompletedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(_mapper.Map<FreelancerViewModel>(notification.Freelancer));
        }
    }
}
