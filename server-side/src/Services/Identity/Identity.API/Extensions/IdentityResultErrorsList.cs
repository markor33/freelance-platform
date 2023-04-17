using Microsoft.AspNetCore.Identity;

namespace Identity.API.Extensions
{
    public static class IdentityResultErrorsList
    {
        public static List<string> ToStringList(this IEnumerable<IdentityError> errors)
        {
            return errors.Select(x => x.Description).ToList();
        }
    }
}
