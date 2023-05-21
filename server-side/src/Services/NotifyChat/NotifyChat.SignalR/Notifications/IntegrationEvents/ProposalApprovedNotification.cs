using EventBus.Events;

namespace NotifyChat.Notifications.IntegrationEvents
{
    public record ProposalApprovedNotification : IntegrationEvent
    {
        public Guid JobId { get; private init; }
        public string JobTitle { get; private init; }
        public Guid ProposalId { get; private init; }
        public Guid FreelancerId { get; private init; }

        public ProposalApprovedNotification(Guid jobId, string jobTitle, Guid proposalId, Guid freelancerId)
        {
            JobId = jobId;
            JobTitle = jobTitle;
            ProposalId = proposalId;
            FreelancerId = freelancerId;
        }

    }
}
