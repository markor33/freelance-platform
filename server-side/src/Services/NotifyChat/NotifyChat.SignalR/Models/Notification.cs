using MongoDB.Bson.Serialization.Attributes;

namespace NotifyChat.SignalR.Models
{
    public class Notification<TNotfData>
    {
        [BsonId]
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Type { get; private set; }
        public TNotfData Data { get; private set; }
        public bool IsChecked { get; private set; }

        public Notification() { }

        public Notification(Guid userId, string type, TNotfData data)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Type = type;
            Data = data;
            IsChecked = false;
        }

        public void SetChecked() => IsChecked = true;

    }
}
