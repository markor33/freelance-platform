using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class ProfilePictureChangedDomainEventHandler : INotificationHandler<ProfilePictureChanged>
    {
        private readonly IFreelancerReadModelRepository _repository;

        public ProfilePictureChangedDomainEventHandler(IFreelancerReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(ProfilePictureChanged notification, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(notification.AggregateId, fr => fr.ProfilePictureUrl, notification.ProfilePictureUrl);
        }
    }
}
