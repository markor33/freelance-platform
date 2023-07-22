using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Web.Bff.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUserId(this IEnumerable<Claim> claims)
        {
            var userIdClaim = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim is null)
                return string.Empty;
            return userIdClaim.Value;
        }

        public static string GetRole(this IEnumerable<Claim> claims)
        {
            var roleClaim = claims.FirstOrDefault(c => c.Type == "role");
            if (roleClaim is null)
                return string.Empty;
            return roleClaim.Value;
        }

        public static string GetUserDomainId(this IEnumerable<Claim> claims)
        {
            var userDomainIdClaim = claims.FirstOrDefault(c => c.Type == "domainUserId");
            if (userDomainIdClaim is null)
                return string.Empty;
            return userDomainIdClaim.Value;
        }
    }
}
