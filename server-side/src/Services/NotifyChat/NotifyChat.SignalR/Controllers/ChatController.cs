using EventBus.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotifyChat.SignalR.Hubs;
using NotifyChat.SignalR.IntegrationEvents;
using NotifyChat.SignalR.Models;
using NotifyChat.SignalR.Persistence.Repositories;

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

        public ChatController(
            IChatRepository chatRepository,
            IMessageRepository messageRepository,
            IHubContext<ChatHub> chatHub,
            IEventBus eventBus)
        {
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _chatHub = chatHub;
            _eventBus = eventBus;
        }

        [HttpGet("{id}/messages")]
        public async Task<ActionResult<List<Message>>> GetMessages(Guid id)
        {
            var messages = await _messageRepository.GetByChat(id);
            return Ok(messages);
        }

        [HttpPost]
        // [Authorize(Roles = "CLIENT")]
        public async Task<ActionResult<Chat>> Create(CreateChatRequest request)
        {
            var chat = new Chat(request.ClientId, request.FreelancerId, request.JobId);
            await _chatRepository.Create(chat);

            var message = new Message(chat.Id, request.InitialMessage);
            await _messageRepository.Create(message);
            await _chatHub.Clients.Groups(request.FreelancerId.ToString()).SendAsync(message.Text);

            _eventBus.Publish(new InitialMessageSentIntegrationEvent(request.JobId, request.ProposalId));

            return Ok(chat);
        }

    }

    public class CreateChatRequest
    {
        public Guid JobId { get; set; }
        public Guid ProposalId { get; set; }
        public Guid ClientId { get; set; }
        public Guid FreelancerId { get; set; }
        public string InitialMessage { get; set; } = string.Empty;
    }
}
