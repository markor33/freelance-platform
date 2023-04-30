using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Bff.Models;
using Web.Bff.Services;

namespace Web.Bff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IFreelancerProfileService _freelancerProfileService;
        private readonly IProposalService _proposalService;

        public JobController(IFreelancerProfileService freelancerProfileService, IProposalService proposalService)
        {
            _freelancerProfileService = freelancerProfileService;
            _proposalService = proposalService;
        }

        [HttpGet("{id}/proposal")]
        [Authorize(Roles = "CLIENT")]
        public async Task<ActionResult<List<Proposal>>> Test(Guid id)
        {
            var result = await _proposalService.GetProposalsByJobIdAsync(id.ToString());
            var proposals = new List<Proposal>();
            foreach (var proposal in result)
            {
                var freelancer = await _freelancerProfileService.GetBasicDataByIdAsync(proposal.FreelancerId);
                proposals.Add(new Proposal(proposal, freelancer));
            }
            return Ok(proposals);
        }
    }
}
