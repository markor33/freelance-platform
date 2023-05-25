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
    public class CertificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public CertificationController(
            IMediator mediator, 
            IIdentityService identityService,
            IMapper mapper)
        {
            _mediator = mediator;
            _identityService = identityService;
            _mapper = mapper;
        }

        [HttpPut("{certificationId}")]
        [Authorize(Roles = "FREELANCER"), ProfileOwnerAuthorization]
        public async Task<ActionResult<CertificationViewModel>> Update(EditCertificationCommand command)
        {
            var freelancerId = _identityService.GetDomainUserId();
            command.FreelancerId = freelancerId;
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok(_mapper.Map<CertificationViewModel>(commandResult.Value));
        }

        [HttpDelete("{certificationId}")]
        [Authorize(Roles = "FREELANCER"), ProfileOwnerAuthorization]
        public async Task<ActionResult> Delete(Guid id, Guid certificationId)
        {
            var command = new DeleteCertificationCommand(id, certificationId);
            var commandResult = await _mediator.Send(command);
            if (commandResult.IsFailed)
                return BadRequest(commandResult.Errors.ToStringList());
            return Ok();
        }

    }
}
