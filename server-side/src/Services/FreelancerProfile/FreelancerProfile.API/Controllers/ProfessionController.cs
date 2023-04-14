using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entites;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionController : ControllerBase
    {
        private readonly ISkillQueries _skillQueries;

        public ProfessionController(ISkillQueries skillQueries)
        {
            _skillQueries = skillQueries;
        }

        [HttpGet("{id}/skills")]
        public async Task<ActionResult<List<Skill>>> GetSkills(string id)
            => await _skillQueries.GetByProfession(Guid.Parse(id));
    }
}
