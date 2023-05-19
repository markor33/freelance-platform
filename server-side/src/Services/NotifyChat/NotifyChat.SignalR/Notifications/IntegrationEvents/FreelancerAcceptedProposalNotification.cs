using EventBus.Events;

namespace NotifyChat.Notifications.IntegrationEvents
{
    public record FreelancerAcceptedProposalNotification : IntegrationEvent
    {
        public Guid JobId { get; private init; }
        public string JobTitle { get; private init; }
        public Guid ProposalId { get; private init; }
        public Guid ClientId { get; private init; }

        public FreelancerAcceptedProposalNotification(Guid jobId, string jobTitle, Guid proposalId, Guid clientId)
        {
            JobId = jobId;
            JobTitle = jobTitle;
            ProposalId = proposalId;
            ClientId = clientId;
        }

    }
}
