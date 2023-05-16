namespace NotifyChat.SignalR.Models
{
    public class Message
    {
        public Guid Id { get; private set; }
        public string Text { get; private set; } = string.Empty;
        public DateTime Date { get; private set; }

        public Message(string text)
        {
            Id = Guid.NewGuid();
            Text = text;
            Date = DateTime.UtcNow;
        }
    }
}
