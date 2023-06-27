using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using MediatR;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class ProfileSummaryUpdatedDomainEvent : INotification
    {
        public Guid FreelancerId { get; private set; }
        public ProfileSummary ProfileSummary { get; private set; }

        public ProfileSummaryUpdatedDomainEvent(Guid freelancerId, ProfileSummary profileSummary)
        {
            FreelancerId = freelancerId;
            ProfileSummary = profileSummary;
        }

    }
}
