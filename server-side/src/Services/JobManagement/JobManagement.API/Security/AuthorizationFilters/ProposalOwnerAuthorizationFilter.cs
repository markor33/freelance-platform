using JobManagement.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobManagement.API.Security.AuthorizationFilters
{
    public class ProposalOwnerAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IJobRepository _jobRepository;
        private readonly IIdentityService _identityService;

        public ProposalOwnerAuthorizationFilter(
            IJobRepository jobRepository,
            IIdentityService identityService)
        {
            _jobRepository = jobRepository;
            _identityService = identityService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userDomainId = _identityService.GetDomainUserId();

            var proposalId = context.RouteData.Values["proposalId"]?.ToString();

            var isProposalOwner = await _jobRepository.IsProposalOwner(Guid.Parse(proposalId), userDomainId);

            if (!isProposalOwner)
                context.Result = new UnauthorizedResult();
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class ProposalOwnerAuthorization : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var jobRepository = serviceProvider.GetRequiredService<IJobRepository>();
            var identityService = serviceProvider.GetRequiredService<IIdentityService>();
            return new ProposalOwnerAuthorizationFilter(jobRepository, identityService); 
        }
    }

}
