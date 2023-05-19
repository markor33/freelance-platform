using EventBus.Abstractions;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using NotifyChat.Notifications.IntegrationEvents;
using NotifyChat.SignalR.Hubs;
using NotifyChat.SignalR.Models;
using NotifyChat.SignalR.Persistence.Repositories;

namespace NotifyChat.SignalR.Notifications.Handlers
{
    public class FreelancerAcceptedProposalNotificationHandler : IIntegrationEventHandler<FreelancerAcceptedProposalNotification>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<NotificationHub> _hubContext;

        public FreelancerAcceptedProposalNotificationHandler(INotificationRepository notificationRepository, IHubContext<NotificationHub> hubContext)
        {
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
        }

        public async Task HandleAsync(FreelancerAcceptedProposalNotification @event)
        {
            var notf = new Notification(@event.ClientId, nameof(FreelancerAcceptedProposalNotification), @event.ToBsonDocument());
            await _notificationRepository.Create(notf);
            await _hubContext.Clients
                .Group(@event.ClientId.ToString())
                .SendAsync("newNotification", notf);
        }
    }
}
