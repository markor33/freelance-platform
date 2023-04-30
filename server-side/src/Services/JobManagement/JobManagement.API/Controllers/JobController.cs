using AutoMapper;
using JobManagement.API.Extensions;
using JobManagement.API.Security;
using JobManagement.Application.Commands;
using JobManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IJobQueries _jobQueries;
        private readonly IProposalQueries _proposalQueries;

        public JobController(
            IMediator mediator,
            IMapper mapper,
            IJobQueries jobQueries,
            IProposalQueries proposalQueries)
        {
            _mediator = mediator;
            _mapper = mapper;
            _jobQueries = jobQueries;
            _proposalQueries = proposalQueries;
        }

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

        [HttpGet("proposal/{proposalId}")]
        public async Task<ActionResult<ProposalViewModel>> GetProposalById(Guid proposalId)
        {
            var result = await _proposalQueries.GetByIdAsync(proposalId);
            if (result.IsFailed)
                return BadRequest(result.Errors.ToStringList());
            return Ok(result.Value);
        }

        [HttpPost("proposal")]
        [Authorize(Roles = "FREELANCER")]
        public async Task<ActionResult<ProposalViewModel>> CreateProposal(CreateProposalCommand command)
        {
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Accepted(_mapper.Map<ProposalViewModel>(commandResult.Value));
        }

    }
}
