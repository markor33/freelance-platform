using MediatR;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class ProfilePictureChangedDomainEvent : INotification
    {
        public Guid FreelancerId { get; private set; }
        public string ProfilePictureUrl { get; private set; }

        public ProfilePictureChangedDomainEvent(Guid freelancerId, string profilePictureUrl)
        {
            FreelancerId = freelancerId;
            ProfilePictureUrl = profilePictureUrl;
        }

    }
}
