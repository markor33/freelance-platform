using EventBus.Events;
using System.Text.Json.Serialization;

namespace NotifyChat.Notifications.IntegrationEvents
{
    public record InterviewStageStartedNotification : IntegrationEvent
    {
        public Guid FreelancerId { get; private init; }
        public Guid JobId { get; private init; }
        public string JobTitle { get; private init; }
        public Guid ProposalId { get; private init; }

        [JsonConstructor]
        public InterviewStageStartedNotification(Guid freelancerId, Guid jobId, string jobTitle, Guid proposalId)
        {
            FreelancerId = freelancerId;
            JobId = jobId;
            JobTitle= jobTitle;
            ProposalId= proposalId;
        }
    }
}
