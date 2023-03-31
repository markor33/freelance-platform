using Identity.API.Models;
using Identity.API.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            var newUser = new ApplicationUser()
            {
                Email = registerViewModel.Email,
                UserName =registerViewModel.Username
            };
            var result =  await _userManager.CreateAsync(newUser, registerViewModel.Password);
            if (!result.Succeeded)
                return result;
            result = await _userManager.AddToRoleAsync(newUser, registerViewModel.Role.ToString());
            return result;
        }
    }
}
