using System.Security.Claims;

namespace FeedbackManagement.API.Security
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public Guid GetDomainUserId()
        {
            return new Guid(_context.HttpContext.User.FindFirst("domainUserId").Value);
        }

        public string GetRole()
        {
            return _context.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
        }

        public Guid GetUserId()
        {
            return new Guid(_context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
