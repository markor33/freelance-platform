using Identity.API.Models.ViewModels;
using Identity.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        public AuthController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _registerService.RegisterAsync(registerViewModel);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok();
        }

    }
}
