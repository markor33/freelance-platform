using System.Security.Claims;

namespace JobManagement.API.Security
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public Guid GetUserId()
        {
            return new Guid(_context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
