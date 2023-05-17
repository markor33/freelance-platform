using NotifyChat.SignalR.Models;

namespace NotifyChat.SignalR.Persistence.Repositories
{
    public interface IChatRepository
    {
        Task<Chat> GetById(Guid id);
        Task Create(Chat chat);
    }
}
