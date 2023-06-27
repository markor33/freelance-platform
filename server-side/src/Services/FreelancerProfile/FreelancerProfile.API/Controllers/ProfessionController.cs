using FreelancerProfile.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionController : ControllerBase
    {
        private readonly IProfessionQueries _professionQueries;

        public ProfessionController(IProfessionQueries professionQueries)
        {
            _professionQueries = professionQueries;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProfessionViewModel>>> Get()
        {
            var professions = await _professionQueries.GetAllAsync();
            return Ok(professions);
        }

        [HttpGet("{id}/skills")]
        public async Task<ActionResult<List<SkillViewModel>>> GetSkills(string id)
        {
            var skills = await _professionQueries.GetSkillsByProfessionAsync(Guid.Parse(id));
            return Ok(skills);
        }

    }
}
