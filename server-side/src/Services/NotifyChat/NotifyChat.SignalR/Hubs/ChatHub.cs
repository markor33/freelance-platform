using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NotifyChat.SignalR.Models;
using NotifyChat.SignalR.Persistence.Repositories;

namespace NotifyChat.SignalR.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;

        public ChatHub(IChatRepository chatRepository, IMessageRepository messageRepository)
        {
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var userDomainId = Context.User.FindFirst("DomainUserId").Value.ToString();

            await Groups.AddToGroupAsync(Context.ConnectionId, userDomainId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userDomainId = Context.User.FindFirst("DomainUserId").Value.ToString();

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userDomainId);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task NewMessage(Guid chatId, string text)
        {
            var userDomainId = Context.User.FindFirst("DomainUserId").Value.ToString();

            var chat = await _chatRepository.GetById(chatId);
            var message = new Message(chatId, Guid.Parse(userDomainId), text);
            await _messageRepository.Create(message);

            var sendToId = (chat.ClientId == Guid.Parse(userDomainId)) ? chat.FreelancerId : chat.ClientId; 

            await Clients.Groups(sendToId.ToString()).SendAsync("newMessage", message);
            await Clients.Groups(userDomainId).SendAsync("newMessageResponse", message);
        }

    }
}
