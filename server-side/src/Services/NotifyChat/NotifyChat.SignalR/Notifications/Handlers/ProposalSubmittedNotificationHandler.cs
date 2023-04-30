using EventBus.Abstractions;
using Microsoft.AspNetCore.SignalR;
using NotifyChat.Notifications.Models;
using NotifyChat.SignalR.Hubs;

namespace NotifyChat.SignalR.Notifications.Handlers
{
    public class ProposalSubmittedNotificationHandler : IIntegrationEventHandler<ProposalSubmittedNotification>
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public ProposalSubmittedNotificationHandler(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        async Task IIntegrationEventHandler<ProposalSubmittedNotification>.HandleAsync(ProposalSubmittedNotification @event)
        {
            await _hubContext.Clients
                .Group(@event.ClientId.ToString())
                .SendAsync(nameof(ProposalSubmittedNotification), new { jobTitle = @event.JobName, jobId = @event.JobId });
        }

    }
}
