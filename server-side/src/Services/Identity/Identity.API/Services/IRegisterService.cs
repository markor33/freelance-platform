using Identity.API.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Services
{
    public interface IRegisterService
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel registerViewModel);
    }
}
