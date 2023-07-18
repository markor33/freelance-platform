using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class ProfileSummaryUpdatedDomainEventHandler : INotificationHandler<ProfileSummaryUpdatedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;

        public ProfileSummaryUpdatedDomainEventHandler(IFreelancerReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(ProfileSummaryUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(notification.FreelancerId, fr => fr.ProfileSummary, notification.ProfileSummary);
        }
    }
}
