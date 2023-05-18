using NotifyChat.SignalR.Models;

namespace NotifyChat.SignalR.Persistence.Repositories
{
    public interface IMessageRepository
    {
        Task<Message> GetById(Guid id);
        Task<List<Message>> GetByChat(Guid chatId);   
        Task Create(Message message);
        Task Update(Message message);
    }
}
