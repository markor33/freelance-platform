using AutoMapper;
using FreelancerProfile.API.Extensions;
using FreelancerProfile.API.Security;
using FreelancerProfile.API.Security.AuthorizationFilters;
using FreelancerProfile.Application.Commands;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
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

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<FreelancerViewModel>> Get(Guid id)
        {
            var queryResult = await _queries.GetByIdAsync(id);
            if (queryResult.IsFailed)
                return BadRequest(queryResult.Errors.ToStringList());
            return Ok(queryResult.Value);
        }

        [HttpPost]
        [Authorize(Roles = "FREELANCER")]
        public async Task<ActionResult<FreelancerViewModel>> Create(CreateFreelancerCommand command)
        {
            command.UserId = _identityService.GetUserId();
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok(_mapper.Map<FreelancerViewModel>(commandResult.Value));
        }

        [HttpPut("{id}/profile-summary")]
        [Authorize(Roles = "FREELANCER"), ProfileOwnerAuthorization]
        public async Task<ActionResult<ProfileSummary>> UpdateProfileSummary(ProfileSummary profileSummary)
        {
            var freelancerId = _identityService.GetDomainUserId();
            var command = new UpdateProfileSummaryCommand(freelancerId, profileSummary);
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok(_mapper.Map<ProfileSummary>(commandResult.Value));
        }

        [HttpPut("{id}/profile-picture"), ProfileOwnerAuthorization]
        public async Task<ActionResult<string>> SetProfilePicture([FromForm] IFormFile profilePicture)
        {
            var freelancerId = _identityService.GetDomainUserId();
            var command = new SetProfilePictureCommand(freelancerId, profilePicture);
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Content(commandResult.Value, "text/plain");
        }

        [HttpPost("skill")]
        public async Task<ActionResult<List<SkillViewModel>>> AddSkill(AddSkillCommand command)
        {
            command.UserId = _identityService.GetUserId();
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok(_mapper.Map<List<SkillViewModel>>(commandResult.Value));
        }

    }
}
