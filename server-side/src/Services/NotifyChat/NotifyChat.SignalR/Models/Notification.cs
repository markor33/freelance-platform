using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotifyChat.SignalR.Models
{
    public class Notification
    {
        [BsonId]
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Type { get; private set; }
        public bool IsChecked { get; private set; }
        public Dictionary<string, object> Data { get; private set; }

        public Notification() { }

        public Notification(Guid userId, string type, BsonDocument data)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Type = type;
            Data = data.ToDictionary();
            IsChecked = false;
        }

        public void SetChecked() => IsChecked = true;

    }
}
