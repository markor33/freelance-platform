using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FreelancerProfile.API.Security.AuthorizationFilters
{
    public class ProfileOwnerAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IIdentityService _identityService;

        public ProfileOwnerAuthorizationFilter(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {

            var userDomainId = _identityService.GetDomainUserId();
            var freelancerId = context.RouteData.Values["id"].ToString();

            if (userDomainId.ToString() != freelancerId)
                context.Result = new UnauthorizedResult();
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class ProfileOwnerAuthorization : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var identityService = serviceProvider.GetRequiredService<IIdentityService>();
            return new ProfileOwnerAuthorizationFilter(identityService);
        }
    }
}
