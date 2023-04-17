using AutoMapper;
using FluentResults;
using FreelancerProfile.API.Extensions;
using FreelancerProfile.API.Security;
using FreelancerProfile.Application.Commands;
using FreelancerProfile.Application.Queries;
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
        private readonly IMapper _mapper;

        public FreelancerController(
            IMediator mediator,
            IFreelancerQueries queries,
            IIdentityService identityService,
            IMapper mapper)
        {
            _mediator = mediator;
            _queries = queries;
            _identityService = identityService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<FreelancerViewModel>> Get()
        {
            var userId = _identityService.GetUserId();
            var queryResult = await _queries.GetFreelancerFromUserAsync(userId);
            if (queryResult.IsFailed)
                return BadRequest(queryResult.Errors.ToStringList());
            return Ok(queryResult.Value);
        }

        [HttpPost]
        public async Task<ActionResult<FreelancerViewModel>> Create(CreateFreelancerCommand command)
        {
            command.UserId = _identityService.GetUserId();
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok(_mapper.Map<FreelancerViewModel>(commandResult.Value));
        }

        [HttpPost("education")]
        public async Task<ActionResult<EducationViewModel>> AddEducation(AddEducationCommand command)
        {
            command.UserId = _identityService.GetUserId();
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok(_mapper.Map<EducationViewModel>(commandResult.Value));
        }

        [HttpPost("certification")]
        public async Task<ActionResult<CertificationViewModel>> AddCertification(AddCertificationCommand command)
        {
            command.UserId = _identityService.GetUserId();
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok(_mapper.Map<CertificationViewModel>(commandResult.Value));
        }

        [HttpPost("employment")]
        public async Task<ActionResult<EmploymentViewModel>> AddEmployment(AddEmploymentCommand command)
        {
            command.UserId = _identityService.GetUserId();
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok(_mapper.Map<EmploymentViewModel>(commandResult.Value));
        }

        [HttpPost("skill")]
        public async Task<IActionResult> AddSkill(AddSkillCommand command)
        {
            command.UserId = _identityService.GetUserId();
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok();
        }

    }
}
