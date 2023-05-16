using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;
using NotifyChat.SignalR.Models;
using NotifyChat.SignalR.Persistence.Settings;

namespace NotifyChat.SignalR.Hubs
{
    // [Authorize]
    public class NotificationHub : Hub
    {
        private readonly IMongoCollection<Notification<object>> _notificationsCollection;

        public NotificationHub(IMongoDbFactory mongoDbFactory)
        {
            _notificationsCollection = mongoDbFactory.GetCollection<Notification<object>>("notifications");
        }

        public override async Task OnConnectedAsync()
        {
            var domainUserId = Context.GetHttpContext().Request.Query["domainUserId"];

            await Groups.AddToGroupAsync(Context.ConnectionId, domainUserId);

            var pastNotifications = await GetNotifications(domainUserId);
            await Clients.Caller.SendAsync("getNotifications", pastNotifications);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            var domainUserId = Context.GetHttpContext().Request.Query["domainUserId"];

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, domainUserId);
            await base.OnDisconnectedAsync(ex);
        }

        private async Task<List<Notification<object>>> GetNotifications(string userId)
        {
            var filter = Builders<Notification<object>>.Filter.Eq(n => n.UserId, Guid.Parse(userId));
            var notifications = await (await _notificationsCollection.FindAsync<Notification<object>>(filter)).ToListAsync();
            return notifications;
        }

    }
}
