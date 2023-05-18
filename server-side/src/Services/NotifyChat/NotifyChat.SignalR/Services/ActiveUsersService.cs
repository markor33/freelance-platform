using System.Collections.Concurrent;

namespace NotifyChat.SignalR.Services
{
    public class ActiveUsersService : IActiveUsersService
    {
        private ConcurrentDictionary<Guid, bool> _activeUsers;

        public ActiveUsersService()
        {
            _activeUsers = new ConcurrentDictionary<Guid, bool>();
        }

        public void UserConnected(Guid userId)
        {
            _activeUsers.TryAdd(userId, true);
        }

        public void UserDisconnected(Guid userId)
        {
            _activeUsers.TryRemove(userId, out bool removedValue);
        }

        public bool IsActive(Guid userId)
        {
            return _activeUsers.TryGetValue(userId, out bool active);
        }

    }
}
