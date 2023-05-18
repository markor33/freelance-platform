using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NotifyChat.SignalR.Persistence.Repositories;

namespace NotifyChat.SignalR.Security.AuthorizationFilters
{
    public class ChatParticipantAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IChatRepository _chatRepository;
        private readonly IIdentityService _identityService;

        public ChatParticipantAuthorizationFilter(IChatRepository chatRepository, IIdentityService identityService)
        {
            _chatRepository = chatRepository;
            _identityService = identityService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userDomainId = _identityService.GetDomainUserId();
            var chatId = context.RouteData.Values["id"].ToString();

            var chat = await _chatRepository.GetById(Guid.Parse(chatId));
            if (chat.FreelancerId != userDomainId && chat.ClientId != userDomainId)
                context.Result = new UnauthorizedResult();
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class ChatParticipantAuthorization : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var chatRepository = serviceProvider.GetRequiredService<IChatRepository>();
            var identityService = serviceProvider.GetRequiredService<IIdentityService>();
            return new ChatParticipantAuthorizationFilter(chatRepository, identityService);
        }
    }
}
