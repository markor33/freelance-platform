﻿using EventBus.Events;
using System.Text.Json.Serialization;

namespace JobManagement.Application.IntegrationEvents.Events
{
    public record JobDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid JobId { get; private set; }

        public JobDeletedIntegrationEvent() { }

        [JsonConstructor]
        public JobDeletedIntegrationEvent(Guid jobId)
        {
            JobId = jobId;
        }

    }
}