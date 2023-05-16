using FluentResults;

namespace Web.Bff.Services
{
    public interface IIdentityService
    {
        Task<Result<string>> LoginAsync(string username, string password);
    }
}
