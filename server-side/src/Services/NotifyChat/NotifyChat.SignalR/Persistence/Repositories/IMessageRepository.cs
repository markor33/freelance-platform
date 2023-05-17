using NotifyChat.SignalR.Models;

namespace NotifyChat.SignalR.Persistence.Repositories
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetByChat(Guid chatId);   
        Task Create(Message message);
    }
}
