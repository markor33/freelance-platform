using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NotifyChat.SignalR.Persistence.Repositories;

namespace NotifyChat.SignalR.Hubs
{
    // [Authorize]
    public class NotificationHub : Hub
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationHub(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var domainUserId = Context.GetHttpContext().Request.Query["domainUserId"];

            await Groups.AddToGroupAsync(Context.ConnectionId, domainUserId);

            var pastNotifications = await _notificationRepository.GetByUser(Guid.Parse(domainUserId));
            await Clients.Caller.SendAsync("getNotifications", pastNotifications);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            var domainUserId = Context.GetHttpContext().Request.Query["domainUserId"];

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, domainUserId);
            await base.OnDisconnectedAsync(ex);
        }

    }
}
