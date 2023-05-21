using JobManagement.API.Extensions;
using JobManagement.API.Security.AuthorizationFilters;
using JobManagement.Application.Commands;
using JobManagement.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobManagement.API.Controllers
{
    public partial class JobController
    {

        [HttpGet("contract/freelancer")]
        [Authorize(Roles = "FREELANCER")]
        public async Task<ActionResult<List<ContractViewModel>>> GetContractsByFreelancer()
        {
            var freelancerId = _identityService.GetDomainUserId();
            return await _contractQueries.GetByFreelancer(freelancerId);
        }

        [HttpPost("{id}/contract/proposal/{proposalId}")]
        [Authorize(Roles = "FREELANCER"), ProposalOwnerAuthorization]
        public async Task<ActionResult> CreateContract(Guid id, Guid proposalId)
        {
            var command = new MakeContractCommand(id, proposalId);
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok();
        }

        [HttpPut("{id}/contract/{contractId}/status/finished")]
        [Authorize(Roles = "CLIENT"), JobOwnerAuthorization]
        public async Task<ActionResult> ChangeContractStatus(Guid id, Guid contractId)
        {
            var command = new FinishContractCommand(id, contractId);
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok();
        }
    }
}
