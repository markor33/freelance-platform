using JobManagement.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobManagement.API.Security.AuthorizationFilters
{
    public class JobOwnerAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IJobRepository _jobRepository;
        private readonly IIdentityService _identityService;

        public JobOwnerAuthorizationFilter(
            IJobRepository jobRepository, 
            IIdentityService identityService)
        {
            _jobRepository = jobRepository;
            _identityService = identityService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userDomainId = _identityService.GetDomainUserId();
            var jobId = context.RouteData.Values["id"].ToString();

            var isJobOwner = await _jobRepository.IsJobOwner(Guid.Parse(jobId), userDomainId);

            if (!isJobOwner)
                context.Result = new UnauthorizedResult();
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class JobOwnerAuthorization : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var jobRepository = serviceProvider.GetRequiredService<IJobRepository>();
            var identityService = serviceProvider.GetRequiredService<IIdentityService>();
            return new JobOwnerAuthorizationFilter(jobRepository, identityService);
        }
    }
}
