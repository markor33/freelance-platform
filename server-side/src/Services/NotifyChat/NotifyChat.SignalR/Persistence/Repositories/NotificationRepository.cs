using MongoDB.Driver;
using NotifyChat.SignalR.Models;
using NotifyChat.SignalR.Persistence.Settings;

namespace NotifyChat.SignalR.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IMongoCollection<Notification> _notificationsCollection;

        public NotificationRepository(IMongoDbFactory mongoDb)
        {
            _notificationsCollection = mongoDb.GetCollection<Notification>("notifications");
        }

        public async Task<Notification> GetById(Guid id)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.Id, id);
            var notification = await(await _notificationsCollection.FindAsync(filter)).FirstOrDefaultAsync();
            return notification;
        }

        public async Task<List<Notification>> GetByUser(Guid userId)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.UserId, userId);
            try
            {
                var notifications = await (await _notificationsCollection.FindAsync<Notification>(filter)).ToListAsync();
                return notifications;
            }
            catch(Exception ex)
            {
                var a = 1;
                return null;
            }
        }

        public async Task Create(Notification notification)
        {
            await _notificationsCollection.InsertOneAsync(notification);
        }

        public async Task Update(Notification notification)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.Id, notification.Id);
            await _notificationsCollection.ReplaceOneAsync(filter, notification);
        }

        public async Task Delete(List<Guid> ids)
        {
            var filter = Builders<Notification>.Filter.In(e => e.Id, ids);
            await _notificationsCollection.DeleteManyAsync(filter);
        }
    }
}
