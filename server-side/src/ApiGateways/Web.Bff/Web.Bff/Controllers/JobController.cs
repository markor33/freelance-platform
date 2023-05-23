using GrpcJobManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Bff.Models;
using Web.Bff.Services;

namespace Web.Bff.Controllers
{
    [Route("api/aggregator/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobManagementService _jobManagementService;
        private readonly IProposalService _proposalService;

        public JobController(
            IJobManagementService jobManagementService,
            IProposalService proposalService)
        {
            _jobManagementService = jobManagementService;
            _proposalService = proposalService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<SearchJob>>> Get([FromQuery] string? queryText = null, [FromQuery] JobSearchFilters? filters = null)
        {
            return Ok(await _jobManagementService.Search(queryText, filters));
        }

        [HttpGet("{id}/proposal")]
        [Authorize(Roles = "CLIENT")]
        public async Task<ActionResult<List<Models.Proposal>>> GetJobProposals(Guid id)
        {
            var proposals = await _proposalService.GetProposalsByJobIdAsync(id.ToString());
            return Ok(proposals);
        }
    }
}
