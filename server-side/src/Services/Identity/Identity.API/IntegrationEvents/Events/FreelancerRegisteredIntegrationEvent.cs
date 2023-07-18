using EventBus.Events;
using Identity.API.Models.ViewModels;
using Newtonsoft.Json;

namespace Identity.API.IntegrationEvents.Events
{
    public record FreelancerRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public Contact Contact { get; init; }

        public FreelancerRegisteredIntegrationEvent()
        {
        }

        [JsonConstructor]
        public FreelancerRegisteredIntegrationEvent(Guid userId, string firstName, string lastName, Contact contact)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Contact = contact;
        }

    }
}
