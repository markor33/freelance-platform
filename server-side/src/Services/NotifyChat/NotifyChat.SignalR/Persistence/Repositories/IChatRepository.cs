using NotifyChat.SignalR.Models;

namespace NotifyChat.SignalR.Persistence.Repositories
{
    public interface IChatRepository
    {
        Task<Chat> GetById(Guid id);
        Task<Chat> GetByClient(Guid clientId);
        Task<Chat> GetByFreelancer(Guid freelancerId);
        Task Create(Chat chat);
    }
}
