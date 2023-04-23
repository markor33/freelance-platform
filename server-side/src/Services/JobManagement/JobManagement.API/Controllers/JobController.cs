using AutoMapper;
using JobManagement.API.Extensions;
using JobManagement.Application.Commands;
using JobManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<List<JobViewModel>>> Get()
        {
            return await _jobQueries.GetAllAsync();
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

        [HttpGet("proposal/{proposalId}")]
        [Authorize(Roles = "FREELANCER")]
        public async Task<ActionResult<ProposalViewModel>> Get(Guid proposalId)
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
