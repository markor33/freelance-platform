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
    [Authorize(Roles = "CLIENT")]
    public class JobController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public JobController(IMediator mediator, IMapper mapper, IIdentityService identityService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<ActionResult<JobViewModel>> Create(CreateJobCommand command)
        {
            command.UserId = _identityService.GetUserId();
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok(_mapper.Map<JobViewModel>(commandResult.Value));
        }

    }
}
