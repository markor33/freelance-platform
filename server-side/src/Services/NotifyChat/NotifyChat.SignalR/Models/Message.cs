using MongoDB.Bson.Serialization.Attributes;

namespace NotifyChat.SignalR.Models
{
    public class Message
    {
        [BsonId]
        public Guid Id { get; private set; }
        public Guid ChatId { get; private set; }
        public string Text { get; private set; } = string.Empty;
        public DateTime Date { get; private set; }

        public Message(Guid chatId, string text)
        {
            Id = Guid.NewGuid();
            ChatId = chatId;
            Text = text;
            Date = DateTime.UtcNow;
        }
    }
}
