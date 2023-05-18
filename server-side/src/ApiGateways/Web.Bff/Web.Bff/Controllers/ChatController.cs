using GrpcNotifyChat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Bff.Extensions;
using Web.Bff.Models;
using Web.Bff.Services;

namespace Bff.Controllers
{
    [Route("api/aggregator/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly ChatService.ChatServiceClient _chatClient;
        private readonly IFreelancerProfileService _freelancerProfileService;
        private readonly IClientProfileService _clientProfileService;
        private readonly IJobManagementService _jobManagementService;

        public ChatController(
            ChatService.ChatServiceClient chatClient,
            IFreelancerProfileService freelancerProfileService,
            IClientProfileService clientProfileService,
            IJobManagementService jobManagementService
            )
        {
            _chatClient = chatClient;
            _clientProfileService = clientProfileService;
            _freelancerProfileService = freelancerProfileService;
            _jobManagementService = jobManagementService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Chat>>> Get()
        {
            var userDomainId = User.Claims.GetUserDomainId();

            var chatsResponse = await _chatClient.GetChatsAsync(new GetChatsRequest() { UserId= userDomainId });
            var chats = new List<Chat>();
            foreach (var chat in chatsResponse.Chats)
            {
                var freelancer = await _freelancerProfileService.GetBasicDataByIdAsync(chat.FreelancerId);
                var client = await _clientProfileService.GetBasicDataByIdAsync(chat.ClientId);
                var job = await _jobManagementService.GetById(chat.JobId);
                chats.Add(new Chat()
                {
                    Id = Guid.Parse(chat.Id),
                    JobId = Guid.Parse(job.Id),
                    JobTitle = job.Title,
                    ProposalId = Guid.Parse(chat.ProposalId),
                    ClientId = Guid.Parse(client.Id),
                    IsClientActive = chat.IsClientActive,
                    ClientName = client.FirstName + " " + client.LastName,
                    FreelancerId = Guid.Parse(freelancer.Id),
                    IsFreelancerActive = chat.IsFreelancerActive,
                    FreelancerName = freelancer.FirstName + " " + freelancer.LastName,
                });
            }
            return Ok(chats);
        }
    }
}
