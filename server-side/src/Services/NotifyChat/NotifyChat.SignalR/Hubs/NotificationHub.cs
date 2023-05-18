using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NotifyChat.SignalR.Persistence.Repositories;
using NotifyChat.SignalR.Services;

namespace NotifyChat.SignalR.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IActiveUsersService _activeUsersService;

        public NotificationHub(
            INotificationRepository notificationRepository,
            IActiveUsersService activeUsersService)
        {
            _notificationRepository = notificationRepository;
            _activeUsersService = activeUsersService;
        }

        public override async Task OnConnectedAsync()
        {
            var userDomainId = Context.User.FindFirst("DomainUserId").Value.ToString();

            await Groups.AddToGroupAsync(Context.ConnectionId, userDomainId.ToString());
            _activeUsersService.UserConnected(Guid.Parse(userDomainId));

            var pastNotifications = await _notificationRepository.GetByUser(Guid.Parse(userDomainId.ToString()));
            await Clients.Caller.SendAsync("getNotifications", pastNotifications);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            var userDomainId = Context.User.FindFirst("DomainUserId").Value.ToString();

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userDomainId.ToString());
            _activeUsersService.UserDisconnected(Guid.Parse(userDomainId));

            await base.OnDisconnectedAsync(ex);
        }

    }
}
