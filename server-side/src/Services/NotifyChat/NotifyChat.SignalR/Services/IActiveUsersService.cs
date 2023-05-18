namespace NotifyChat.SignalR.Services
{
    public interface IActiveUsersService
    {
        void UserConnected(Guid userId);
        void UserDisconnected(Guid userId);
        bool IsActive(Guid userId);
    }
}
