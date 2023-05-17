using Microsoft.AspNetCore.SignalR;
using NotifyChat.SignalR.Models;
using NotifyChat.SignalR.Persistence.Repositories;

namespace NotifyChat.SignalR.Hubs
{
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
            var domainUserId = Context.GetHttpContext().Request.Query["domainUserId"];

            await Groups.AddToGroupAsync(Context.ConnectionId, domainUserId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var domainUserId = Context.GetHttpContext().Request.Query["domainUserId"];

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, domainUserId);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task NewMessage(Guid chatId, string message)
        {
            var chat = await _chatRepository.GetById(chatId);
            if (Context.User.IsInRole("CLIENT"))
                await Clients.Groups(chat.FreelancerId.ToString()).SendAsync(message);
            else
                await Clients.Groups(chat.ClientId.ToString()).SendAsync(message);
            await _messageRepository.Create(new Message(chatId, message));
        }

    }
}
