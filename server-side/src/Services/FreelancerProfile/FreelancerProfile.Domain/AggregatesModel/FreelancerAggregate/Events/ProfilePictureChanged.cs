using FreelancerProfile.Domain.SeedWork;
using MediatR;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class ProfilePictureChanged : DomainEvent, INotification
    {
        public string ProfilePictureUrl { get; private set; }

        [JsonConstructor]
        public ProfilePictureChanged(Guid aggregateId, string profilePictureUrl) : base(aggregateId)
        {
            ProfilePictureUrl = profilePictureUrl;
        }

    }
}
