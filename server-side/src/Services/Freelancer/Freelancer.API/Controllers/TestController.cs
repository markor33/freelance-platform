using Microsoft.AspNetCore.Mvc;

namespace Freelancer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("asdasdasd");
        }
    }
}
