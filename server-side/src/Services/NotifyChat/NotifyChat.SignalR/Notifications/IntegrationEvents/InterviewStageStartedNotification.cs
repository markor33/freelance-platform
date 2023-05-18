﻿using EventBus.Events;

namespace NotifyChat.Notifications.IntegrationEvents
{
    public record InterviewStageStartedNotification : IntegrationEvent
    {
        public Guid FreelancerId { get; private init; }
        public Guid JobId { get; private init; }
        public string JobTitle { get; private init; }
        public Guid ProposalId { get; private init; }

        public InterviewStageStartedNotification(Guid freelancerId, Guid jobId, string jobTitle, Guid proposalId)
        {
            FreelancerId = freelancerId;
            JobId = jobId;
            JobTitle= jobTitle;
            ProposalId= proposalId;
        }
    }
}
