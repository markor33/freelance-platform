namespace FreelancerProfile.API.Security
{
    public interface IIdentityService
    {
        Guid GetUserId();
        Guid GetDomainUserId();
    }
}
