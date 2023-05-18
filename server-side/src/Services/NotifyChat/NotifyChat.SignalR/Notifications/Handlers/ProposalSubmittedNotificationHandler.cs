using EventBus.Abstractions;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using NotifyChat.Notifications.IntegrationEvents;
using NotifyChat.SignalR.Hubs;
using NotifyChat.SignalR.Models;
using NotifyChat.SignalR.Persistence.Repositories;

namespace NotifyChat.SignalR.Notifications.Handlers
{
    public class ProposalSubmittedNotificationHandler : IIntegrationEventHandler<ProposalSubmittedNotification>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<NotificationHub> _hubContext;

        public ProposalSubmittedNotificationHandler(
            INotificationRepository notificationRepository,
            IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
            _notificationRepository = notificationRepository;
        }

        async Task IIntegrationEventHandler<ProposalSubmittedNotification>.HandleAsync(ProposalSubmittedNotification @event)
        {
            var notfData = new ProposalSubmittedNotificationData(@event.JobId, @event.JobName);
            var notf = new Notification(@event.ClientId, nameof(ProposalSubmittedNotification), notfData.ToBsonDocument());
            await _notificationRepository.Create(notf);
            await _hubContext.Clients
                .Group(@event.ClientId.ToString())
                .SendAsync("newNotification", notf);
        }

    }
}
