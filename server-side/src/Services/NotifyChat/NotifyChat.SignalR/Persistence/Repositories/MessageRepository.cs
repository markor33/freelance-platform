using MongoDB.Driver;
using NotifyChat.SignalR.Models;
using NotifyChat.SignalR.Persistence.Settings;

namespace NotifyChat.SignalR.Persistence.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoCollection<Message> _messageCollection;

        public MessageRepository(IMongoDbFactory mongoDb)
        {
            _messageCollection = mongoDb.GetCollection<Message>("messages");
        }

        public async Task<Message> GetById(Guid id)
        {
            var filter = Builders<Message>.Filter.Eq(m => m.Id, id);
            return await (await _messageCollection.FindAsync(filter)).FirstOrDefaultAsync();
        }

        public async Task<List<Message>> GetByChat(Guid chatId)
        {
            var filter = Builders<Message>.Filter.Eq(m => m.ChatId, chatId);
            return await (await _messageCollection.FindAsync(filter)).ToListAsync();
        }

        public async Task Create(Message message)
        {
            await _messageCollection.InsertOneAsync(message);
        }

        public async Task Update(Message message)
        {
            var filter = Builders<Message>.Filter.Eq(m => m.Id, message.Id);
            await _messageCollection.ReplaceOneAsync(filter, message);
        }
    }
}
