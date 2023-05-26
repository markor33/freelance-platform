﻿using AutoMapper;
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
    public class EmploymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public EmploymentController(
            IMediator mediator,
            IIdentityService identityService,
            IMapper mapper)
        {
            _mediator = mediator;
            _identityService = identityService;
            _mapper = mapper;
        }

        [HttpPut("{employmentId}")]
        [Authorize(Roles = "FREELANCER"), ProfileOwnerAuthorization]
        public async Task<ActionResult<EmploymentViewModel>> Update(EditEmploymentCommand command)
        {
            var freelancerId = _identityService.GetDomainUserId();
            command.FreelancerId = freelancerId;
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok(_mapper.Map<EmploymentViewModel>(commandResult.Value));
        }

        [HttpDelete("{employmentId}")]
        [Authorize(Roles = "FREELANCER"), ProfileOwnerAuthorization]
        public async Task<ActionResult> Delete(Guid id, Guid employmentId)
        {
            var command = new DeleteEmploymentCommand(id, employmentId);
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok();
        }

    }
}
