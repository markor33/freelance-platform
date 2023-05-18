namespace NotifyChat.SignalR.Security
{
    public interface IIdentityService
    {
        Guid GetUserId();
        Guid GetDomainUserId();
        string GetRole();
    }
}
