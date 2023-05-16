using EventBus.Abstractions;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;
using NotifyChat.Notifications.IntegrationEvents;
using NotifyChat.SignalR.Hubs;
using NotifyChat.SignalR.Models;
using NotifyChat.SignalR.Persistence.Settings;

namespace NotifyChat.SignalR.Notifications.Handlers
{
    public class ProposalSubmittedNotificationHandler : IIntegrationEventHandler<ProposalSubmittedNotification>
    {
        private readonly IMongoCollection<Notification<ProposalSubmittedNotificationData>> _notificationsCollection;
        private readonly IHubContext<NotificationHub> _hubContext;

        public ProposalSubmittedNotificationHandler(
            IMongoDbFactory mongoDb,
            IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
            _notificationsCollection = mongoDb.GetCollection<Notification<ProposalSubmittedNotificationData>>("notifications");
        }

        async Task IIntegrationEventHandler<ProposalSubmittedNotification>.HandleAsync(ProposalSubmittedNotification @event)
        {
            var notfData = new ProposalSubmittedNotificationData(@event.JobId, @event.JobName);
            var notf = new Notification<ProposalSubmittedNotificationData>(@event.ClientId, nameof(ProposalSubmittedNotification), notfData);
            await _notificationsCollection.InsertOneAsync(notf);
            await _hubContext.Clients
                .Group(@event.ClientId.ToString())
                .SendAsync("newNotification", notf);
        }

    }
}
