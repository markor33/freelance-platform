using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class ProfilePictureChangedDomainEventHandler : INotificationHandler<ProfilePictureChangedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;

        public ProfilePictureChangedDomainEventHandler(IFreelancerReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(ProfilePictureChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(notification.FreelancerId, fr => fr.ProfilePictureUrl, notification.ProfilePictureUrl);
        }
    }
}
