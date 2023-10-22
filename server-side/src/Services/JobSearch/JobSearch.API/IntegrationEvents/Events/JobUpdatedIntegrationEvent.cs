using EventBus.Events;
using JobSearch.Abstractions.Model;
using System.Text.Json.Serialization;

namespace JobSearch.API.IntegrationEvents.Events
{
    public record JobUpdatedIntegrationEvent : IntegrationEvent
    {
        public Guid JobId { get; private set; }
        public Guid ClientId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime Created { get; private set; }
        public int Credits { get; private set; }
        public ExperienceLevel ExperienceLevel { get; private set; }
        public Payment Payment { get; private set; }
        public JobStatus Status { get; private set; }
        public Guid ProfessionId { get; private set; }
        public List<Guid> Skills { get; private set; }

        public JobUpdatedIntegrationEvent() { }

        [JsonConstructor]
        public JobUpdatedIntegrationEvent(
            Guid jobId, Guid clientId, string title, string description, 
            DateTime created, int credits, ExperienceLevel experienceLevel, Payment payment, 
            JobStatus status, Guid professionId, List<Guid> skills)
        {
            JobId = jobId;
            ClientId = clientId;
            Title = title;
            Description = description;
            Created = created;
            Credits = credits;
            ExperienceLevel = experienceLevel;
            Payment = payment;
            Status = status;
            ProfessionId = professionId;
            Skills = skills;
        }

    }

}
