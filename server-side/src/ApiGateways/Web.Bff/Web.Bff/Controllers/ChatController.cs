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
            foreach (var chatDTO in chatsResponse.Chats)
            {
                var freelancer = await _freelancerProfileService.GetBasicDataByIdAsync(chatDTO.FreelancerId);
                var client = await _clientProfileService.GetBasicDataByIdAsync(chatDTO.ClientId);
                var job = await _jobManagementService.GetById(chatDTO.JobId);
                var chat = new Chat()
                {
                    Id = Guid.Parse(chatDTO.Id),
                    JobId = Guid.Parse(job.Id),
                    JobTitle = job.Title,
                    ProposalId = Guid.Parse(chatDTO.ProposalId),
                    ClientId = Guid.Parse(client.Id),
                    IsClientActive = chatDTO.IsClientActive,
                    ClientName = client.FirstName + " " + client.LastName,
                    FreelancerId = Guid.Parse(freelancer.Id),
                    IsFreelancerActive = chatDTO.IsFreelancerActive,
                    FreelancerName = freelancer.FirstName + " " + freelancer.LastName,
                };
                if (!string.IsNullOrEmpty(chatDTO.ContractId))
                    chat.ContractId = Guid.Parse(chatDTO.ContractId);
                chats.Add(chat);
            }
            return Ok(chats);
        }
    }
}
