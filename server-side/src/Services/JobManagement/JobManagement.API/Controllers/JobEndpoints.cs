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
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<JobViewModel>>> Get()
        {
            return await _jobQueries.GetAllAsync();
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<ActionResult<List<JobViewModel>>> Get([FromQuery] string? queryText = null, [FromQuery]JobSearchFilters? filters = null)
        {
            return await _jobQueries.Search(queryText, filters);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<JobViewModel>> Get(Guid id)
        {
            return await _jobQueries.GetByIdAsync(id);
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

        [HttpPut("{id}/status/done")]
        [Authorize(Roles = "CLIENT"), JobOwnerAuthorization]
        public async Task<ActionResult> Done(Guid id)
        {
            var command = new JobDoneCommand(id);
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "CLIENT"), JobOwnerAuthorization]
        public async Task<ActionResult> Delete(Guid id)
        {
            var commandResult = await _mediator.Send(new DeleteJobCommand(id));
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok();
        }
    }
}
