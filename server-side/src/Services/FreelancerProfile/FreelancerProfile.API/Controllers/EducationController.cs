using AutoMapper;
using FreelancerProfile.API.Extensions;
using FreelancerProfile.API.Security;
using FreelancerProfile.API.Security.AuthorizationFilters;
using FreelancerProfile.Application.Commands;
using FreelancerProfile.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerProfile.API.Controllers
{
    [Route("api/freelancer/{id}/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public EducationController(
            IMediator mediator,
            IIdentityService identityService,
            IMapper mapper)
        {
            _mediator = mediator;
            _identityService = identityService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "FREELANCER"), ProfileOwnerAuthorization]
        public async Task<ActionResult<EducationViewModel>> AddEducation(AddEducationCommand command)
        {
            command.FreelancerId = _identityService.GetDomainUserId();
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok(_mapper.Map<EducationViewModel>(commandResult.Value));
        }

        [HttpPut("{educationId}")]
        [Authorize(Roles = "FREELANCER"), ProfileOwnerAuthorization]
        public async Task<ActionResult> Update(UpdateEducationCommand command)
        {
            var freelancerId = _identityService.GetDomainUserId();
            command.FreelancerId = freelancerId;
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok();
        }

        [HttpDelete("{educationId}")]
        [Authorize(Roles = "FREELANCER"), ProfileOwnerAuthorization]
        public async Task<ActionResult> Delete(Guid id, Guid educationId)
        {
            var command = new DeleteEducationCommand(id, educationId);
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok();
        }
    }
}
