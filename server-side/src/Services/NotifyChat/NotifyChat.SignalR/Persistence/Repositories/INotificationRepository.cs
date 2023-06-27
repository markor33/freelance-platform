using NotifyChat.SignalR.Models;

namespace NotifyChat.SignalR.Persistence.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> GetById(Guid id);
        Task<List<Notification>> GetByUser(Guid userId);
        Task Create(Notification notification);
        Task Update(Notification notification);
        Task Delete(List<Guid> ids);

    }
}
