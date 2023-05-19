using JobManagement.API.Extensions;
using JobManagement.API.Security.AuthorizationFilters;
using JobManagement.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobManagement.API.Controllers
{
    public partial class JobController
    {
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
    }
}
