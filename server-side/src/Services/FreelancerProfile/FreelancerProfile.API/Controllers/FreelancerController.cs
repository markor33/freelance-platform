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
    [Authorize(Roles = "FREELANCER")]
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
        public async Task<ActionResult<FreelancerViewModel>> Get()
        {
            var userId = _identityService.GetUserId();
            var freelancer = await _queries.GetFreelancerFromUserAsync(userId);
            if (freelancer is null)
                return BadRequest();
            return Ok(freelancer);
        }

        [HttpPost]
        public async Task<ActionResult<Freelancer>> Create(CreateFreelancerCommand command)
        {
            command.UserId = _identityService.GetUserId();
            var commandResult = await _mediator.Send(command);
            if (commandResult is null)
                return BadRequest();
            return Ok(commandResult);
        }

        [HttpPost("education")]
        public async Task<ActionResult<Education>> AddEducation(AddEducationCommand command)
        {
            command.UserId = _identityService.GetUserId();
            var commandResult = await _mediator.Send(command);
            if (commandResult is null)
                return BadRequest();
            return Ok(commandResult);
        }

        [HttpPost("certification")]
        public async Task<ActionResult<Certification>> AddCertification(AddCertificationCommand command)
        {
            command.UserId = _identityService.GetUserId();
            var commandResult = await _mediator.Send(command);
            if (commandResult is null)
                return BadRequest();
            return Ok(commandResult);
        }

        [HttpPost("employment")]
        public async Task<ActionResult<Employment>> AddEmployment(AddEmploymentCommand command)
        {
            command.UserId = _identityService.GetUserId();
            var commandResult = await _mediator.Send(command);
            if (commandResult is null)
                return BadRequest();
            return Ok(commandResult);
        }

        [HttpPost("skill")]
        public async Task<IActionResult> AddSkill(AddSkillCommand command)
        {
            command.UserId = _identityService.GetUserId();
            var commandResult = await _mediator.Send(command);
            if (!commandResult)
                return BadRequest();
            return Ok();
        }

    }
}
