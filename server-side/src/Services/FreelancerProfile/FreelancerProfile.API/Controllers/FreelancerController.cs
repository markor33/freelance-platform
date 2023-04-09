using FreelancerProfile.API.Security;
using FreelancerProfile.Application.Commands;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entites;
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
        private readonly IFreelancerQueries _queries;
        private readonly IIdentityService _identityService;

        public FreelancerController(
            IMediator mediator,
            IFreelancerQueries queries,
            IIdentityService identityService)
        {
            _mediator = mediator;
            _queries = queries;
            _identityService = identityService;
        }

        [HttpGet]
        [Authorize(Roles = "FREELANCER")]
        public async Task<ActionResult<Freelancer>> Get()
        {
            var userId = _identityService.GetUserId();
            var freelancer = await _queries.GetFreelancerFromUserAsync(userId);
            if (freelancer is null)
                return BadRequest();
            return Ok(freelancer);
        }

        [HttpPost]
        [Authorize(Roles = "FREELANCER")]
        public async Task<ActionResult<Freelancer>> Create(CreateFreelancerCommand command)
        {
            command.UserId = _identityService.GetUserId();
            var commandResult = await _mediator.Send(command);
            if (commandResult is null)
                return BadRequest();
            return Ok(commandResult);
        }

        [HttpPost("education")]
        [Authorize(Roles = "FREELANCER")]
        public async Task<ActionResult<Education>> AddEducation(AddEducationCommand command)
        {
            command.UserId = _identityService.GetUserId();
            var commandResult = await _mediator.Send(command);
            if (commandResult is null)
                return BadRequest();
            return Ok(commandResult);
        }

    }
}
