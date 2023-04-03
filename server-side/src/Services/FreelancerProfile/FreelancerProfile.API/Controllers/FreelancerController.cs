using FreelancerProfile.API.Security;
using FreelancerProfile.Application.Commands;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;

        public FreelancerController(IMediator mediator, IIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }

        [HttpPost]
        [Authorize(Roles = "FREELANCER")]
        public async Task<ActionResult<Freelancer>> Create(CreateFreelancerCommand command)
        {
            command.UserId = _identityService.GetUserdId();
            var commandResult = await _mediator.Send(command);
            if (commandResult is null)
                return BadRequest();
            return Ok(commandResult);
        }

    }
}
