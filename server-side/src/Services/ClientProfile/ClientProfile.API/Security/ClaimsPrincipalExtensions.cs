using System.Security.Claims;

namespace ClientProfile.API.Security
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal User)
        {
            return new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
