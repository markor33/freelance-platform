using MongoDB.Driver;

namespace NotifyChat.SignalR.Persistence.Settings
{
    public interface IMongoDbFactory
    {
        public IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
