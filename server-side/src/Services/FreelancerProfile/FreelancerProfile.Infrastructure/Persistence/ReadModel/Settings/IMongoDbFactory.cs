using MongoDB.Driver;

namespace FreelancerProfile.Infrastructure.Persistence.ReadModel.Settings
{
    public interface IMongoDbFactory
    {
        public IMongoCollection<T> GetCollection<T>();
        public IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
