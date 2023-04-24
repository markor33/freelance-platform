using FreelancerProfile.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageQueries _languageQueries;

        public LanguageController(ILanguageQueries languageQueries)
        {
            _languageQueries = languageQueries;
        }

        [HttpGet]
        public async Task<ActionResult<List<LanguageViewModel>>> Get()
        {
            var languages = await _languageQueries.GetAllAsync();
            return Ok(languages);
        }

    }
}
