using FeedbackManagement.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FeedbackManagement.API.Security.AuthorizationFilters
{
    public class ContractMemberAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IFinishedContractRepository _finishedContractRepository;
        private readonly IIdentityService _identityService;

        public ContractMemberAuthorizationFilter(IFinishedContractRepository finishedContractRepository, IIdentityService identityService)
        {
            _finishedContractRepository = finishedContractRepository;
            _identityService = identityService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userDomainId = _identityService.GetDomainUserId();
            var contractId = context.RouteData.Values["id"].ToString();

            var contract = await _finishedContractRepository.GetById(Guid.Parse(contractId));
            if (contract is null)
                context.Result = new BadRequestResult();
            else if (contract.ClientId != userDomainId && contract.FreelancerId != userDomainId)
                context.Result = new UnauthorizedResult();
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class ContractMemberAuthorization : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var finishedContractRepository = serviceProvider.GetRequiredService<IFinishedContractRepository>();
            var identityService = serviceProvider.GetRequiredService<IIdentityService>();
            return new ContractMemberAuthorizationFilter(finishedContractRepository, identityService);
        }
    }

}
