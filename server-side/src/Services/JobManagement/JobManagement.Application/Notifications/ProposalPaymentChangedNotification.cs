﻿using EventBus.Events;

namespace JobManagement.Application.Notifications
{
    public record ProposalPaymentChangedNotification : IntegrationEvent
    {
        public Guid JobId { get; private init; }
        public string JobTitle { get; private init; }
        public Guid ProposalId { get; private init; }
        public Guid FreelancerId { get; private init; }

        public ProposalPaymentChangedNotification(Guid jobId, string jobTitle, Guid proposalId, Guid freelancerId)
        {
            JobId = jobId;
            JobTitle = jobTitle;
            ProposalId = proposalId;
            FreelancerId = freelancerId;
        }
    }
}
