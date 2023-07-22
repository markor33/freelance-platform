using Identity.API.Constants;
using Identity.API.GrpcServices;
using Identity.API.Models;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Identity.API.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserBasicDomainDataService _userBasicDomainDataService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileService(
            IUserBasicDomainDataService userBasicDomainDataService,
            UserManager<ApplicationUser> userManager)
        {
            _userBasicDomainDataService = userBasicDomainDataService;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var role = (await _userManager.GetRolesAsync(user)).First();

            var basicData = await _userBasicDomainDataService.GetBasicDataAsync(user.Id, role);

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Role, role),
                new Claim(CustomClaimTypes.DomainUserId, basicData.DomainId.ToString()),
                new Claim(CustomClaimTypes.FirstName, basicData.FirstName),
                new Claim(CustomClaimTypes.LastName, basicData.LastName)
            };

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
        }
    }
}
