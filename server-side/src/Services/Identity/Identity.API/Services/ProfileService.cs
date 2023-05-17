using GrpcClientProfile;
using GrpcFreelancerProfile;
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
        private readonly FreelancerProfile.FreelancerProfileClient _freelancerProfileClient;
        private readonly ClientProfile.ClientProfileClient _clientProfileClient;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileService(
            FreelancerProfile.FreelancerProfileClient freelancerClient, 
            ClientProfile.ClientProfileClient clientProfileClient,
            UserManager<ApplicationUser> userManager)
        {
            _freelancerProfileClient = freelancerClient;
            _clientProfileClient = clientProfileClient;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var role = (await _userManager.GetRolesAsync(user)).First();
            string? domainUserId;
            if (role == "CLIENT")
            {
                var request = new GetClientBasicDataByUserIdRequest() { UserId = user.Id.ToString() };
                var clientData = await _clientProfileClient.GetClientBasicDataByUserIdAsync(request);
                domainUserId = clientData.Id;
            }
            else
            {
                var request = new GetFreelancerBasicDataByUserIdRequest() { UserId = user.Id.ToString() };
                var freelancerData = await _freelancerProfileClient.GetFreelancerBasicDataByUserIdAsync(request);
                domainUserId = freelancerData.Id;
            }
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Role, role),
                new Claim("DomainUserId", domainUserId)
            };
            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
        }
    }
}
