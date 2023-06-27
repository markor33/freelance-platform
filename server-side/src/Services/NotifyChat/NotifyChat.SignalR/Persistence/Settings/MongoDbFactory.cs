using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace NotifyChat.SignalR.Persistence.Settings
{
    public class MongoDbFactory : IMongoDbFactory
    {
        private readonly IMongoDatabase _database;

        public MongoDbFactory(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            _database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

    }
}
