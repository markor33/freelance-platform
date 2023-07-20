using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class ProfileSummaryUpdatedDomainEventHandler : INotificationHandler<ProfileSummaryUpdated>
    {
        private readonly IFreelancerReadModelRepository _repository;

        public ProfileSummaryUpdatedDomainEventHandler(IFreelancerReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(ProfileSummaryUpdated notification, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(notification.AggregateId, fr => fr.ProfileSummary, notification.ProfileSummary);
        }
    }
}
