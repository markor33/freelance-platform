namespace FeedbackManagement.API.Security
{
    public interface IIdentityService
    {
        Guid GetUserId();
        Guid GetDomainUserId();
        string GetRole();
    }
}
