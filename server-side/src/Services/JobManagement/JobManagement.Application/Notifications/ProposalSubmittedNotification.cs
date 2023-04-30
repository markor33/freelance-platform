﻿using EventBus.Events;

namespace JobManagement.Application.Notifications
{
    record class ProposalSubmittedNotification : IntegrationEvent
    {
        public Guid ClientId { get; private init; }
        public Guid JobId { get; private init; }
        public string JobName { get; private init; }

        public ProposalSubmittedNotification(Guid clientId, Guid jobId, string jobName)
        {
            ClientId = clientId;
            JobId = jobId;
            JobName = jobName;
        }

    }
}
