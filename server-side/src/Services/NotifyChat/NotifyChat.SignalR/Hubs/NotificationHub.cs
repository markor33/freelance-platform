using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace NotifyChat.SignalR.Hubs
{
    // [Authorize]
    public class NotificationHub : Hub
    {

        public override async Task OnConnectedAsync()
        {
            var domainUserId = Context.GetHttpContext().Request.Query["domainUserId"];

            await Groups.AddToGroupAsync(Context.ConnectionId, domainUserId);
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
