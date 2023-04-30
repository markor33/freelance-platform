using JobManagement.API.Extensions;
using JobManagement.Application.Commands;
using JobManagement.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobManagement.API.Controllers
{
    public partial class JobController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<JobViewModel>>> Get()
        {
            return await _jobQueries.GetAllAsync();
        }

        [HttpGet("client/{clientId}")]
        [Authorize(Roles = "CLIENT")]
        public async Task<ActionResult<List<JobViewModel>>> GetByClient(Guid clientId)
        {
            return await _jobQueries.GetByClientAsync(clientId);
        }

        [HttpPost]
        [Authorize(Roles = "CLIENT")]
        public async Task<ActionResult<JobViewModel>> Create(CreateJobCommand command)
        {
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok(_mapper.Map<JobViewModel>(commandResult.Value));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "CLIENT")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var commandResult = await _mediator.Send(new DeleteJobCommand(id));
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok();
        }
    }
}
