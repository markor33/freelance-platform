using EventBus.Abstractions;
using FeedbackManagement.API.Models;
using FeedbackManagement.API.Notifications;
using FeedbackManagement.API.Persistence;
using FeedbackManagement.API.Security;
using FeedbackManagement.API.Security.AuthorizationFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinishedContractController : ControllerBase
    {
        private readonly IFinishedContractRepository _finishedContractRepository;
        private readonly IEventBus _eventBus;
        private readonly IIdentityService _identityService;

        public FinishedContractController
            (IFinishedContractRepository finishedContractRepository,
            IEventBus eventBus,
            IIdentityService identityService)
        {
            _finishedContractRepository = finishedContractRepository;
            _eventBus = eventBus;
            _identityService = identityService;
        }

        [HttpGet("{id}")]
        [Authorize, ContractMemberAuthorization]
        public async Task<ActionResult<FinishedContract>> Get(Guid id)
        {
            return await _finishedContractRepository.GetById(id);
        }

        [HttpPost("{id}/feedback")]
        [Authorize, ContractMemberAuthorization]
        public async Task<ActionResult> CreateFeedback(Guid id, [FromBody] Feedback feedback)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var finishedContract = await _finishedContractRepository.GetById(id);
            if (finishedContract is null)
                return BadRequest();

            var notification = new FeedbackSubmittedNotification(finishedContract.Id);
            if (_identityService.GetRole() == "CLIENT")
            {
                finishedContract.SetClientFeedback(feedback);
                notification.UserId = finishedContract.FreelancerId;
            }
            else
            {
                finishedContract.SetFreelancerFeedback(feedback);
                notification.UserId = finishedContract.ClientId;
            }
            await _finishedContractRepository.Save();

            _eventBus.Publish(notification);

            return Ok();
        }
    }
}
