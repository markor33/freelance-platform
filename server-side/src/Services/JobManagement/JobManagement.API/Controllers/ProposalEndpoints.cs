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
        [HttpPost("proposal")]
        [Authorize(Roles = "FREELANCER")]
        public async Task<ActionResult<ProposalViewModel>> CreateProposal(CreateProposalCommand command)
        {
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Accepted(_mapper.Map<ProposalViewModel>(commandResult.Value));
        }

        [HttpGet("proposal/{proposalId}")]
        public async Task<ActionResult<ProposalViewModel>> GetProposalById(Guid proposalId)
        {
            var result = await _proposalQueries.GetByIdAsync(proposalId);
            if (result is null)
                return BadRequest();
            return Ok(result);
        }

        [HttpGet("proposal/{proposalId}/answers")]
        public async Task<ActionResult<List<AnswerViewModel>>> GetProposalsAnswers(Guid proposalId)
        {
            return await _answerQueries.GetByProposalAsync(proposalId);
        }

        [HttpPut("{id}/proposal/{proposalId}/payment")]
        [Authorize(Roles = "CLIENT"), JobOwnerAuthorization]
        public async Task<ActionResult> EditProposalPayment([FromBody] EditProposalPaymentCommand command)
        {
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok();
        }

    }
}
