using EventBus.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotifyChat.SignalR.Hubs;
using NotifyChat.SignalR.IntegrationEvents.Events;
using NotifyChat.SignalR.Models;
using NotifyChat.SignalR.Persistence.Repositories;
using NotifyChat.SignalR.Security;
using NotifyChat.SignalR.Security.AuthorizationFilters;

namespace NotifyChat.SignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly IEventBus _eventBus;
        private readonly IIdentityService _identityService;

        public ChatController(
            IChatRepository chatRepository,
            IMessageRepository messageRepository,
            IHubContext<ChatHub> chatHub,
            IEventBus eventBus,
            IIdentityService identityService)
        {
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _chatHub = chatHub;
            _eventBus = eventBus;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<ActionResult<Chat>> Get()
        {
            var userDomainId = _identityService.GetDomainUserId();
            var role = _identityService.GetRole();
            if (role == "CLIENT")
                return await _chatRepository.GetByClient(userDomainId);
            else
                return await _chatRepository.GetByFreelancer(userDomainId);
        }

        [HttpGet("{id}/messages")]
        [ChatParticipantAuthorization]
        public async Task<ActionResult<List<Message>>> GetMessages(Guid id)
        {
            var messages = await _messageRepository.GetByChat(id);
            return Ok(messages);
        }

        [HttpPost]
        [Authorize(Roles = "CLIENT")]
        public async Task<ActionResult<Chat>> Create(CreateChatRequest request)
        {
            var userDomainId = _identityService.GetDomainUserId();

            var chat = new Chat(userDomainId, request.FreelancerId, request.JobId, request.ProposalId);
            await _chatRepository.Create(chat);

            var message = new Message(chat.Id, userDomainId, request.InitialMessage);
            await _messageRepository.Create(message);
            await _chatHub.Clients.Groups(request.FreelancerId.ToString()).SendAsync(message.Text);

            _eventBus.Publish(new InitialMessageSentIntegrationEvent(request.JobId, request.ProposalId, request.FreelancerId));

            return Ok(chat);
        }

    }

    public class CreateChatRequest
    {
        public Guid JobId { get; set; }
        public Guid ProposalId { get; set; }
        public Guid FreelancerId { get; set; }
        public string InitialMessage { get; set; } = string.Empty;
    }
}
