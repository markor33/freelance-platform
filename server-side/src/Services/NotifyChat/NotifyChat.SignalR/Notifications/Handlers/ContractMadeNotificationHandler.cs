using EventBus.Abstractions;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using NotifyChat.Notifications.IntegrationEvents;
using NotifyChat.SignalR.Hubs;
using NotifyChat.SignalR.Models;
using NotifyChat.SignalR.Persistence.Repositories;

namespace NotifyChat.SignalR.Notifications.Handlers
{
    public class ContractMadeNotificationHandler : IIntegrationEventHandler<ContractMadeNotification>
    {
        private readonly IChatRepository _chatRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<NotificationHub> _hubContext;

        public ContractMadeNotificationHandler(
            IChatRepository chatRepository,
            INotificationRepository notificationRepository, 
            IHubContext<NotificationHub> hubContext)
        {
            _chatRepository = chatRepository;
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
        }

        public async Task HandleAsync(ContractMadeNotification @event)
        {
            var chat = await _chatRepository.GetByProposal(@event.ProposalId);
            chat.SetContractId(@event.ContractId);
            await _chatRepository.Update(chat);

            var notf = new Notification(@event.ClientId, nameof(ContractMadeNotification), @event.ToBsonDocument());
            await _notificationRepository.Create(notf);
            await _hubContext.Clients
                .Group(@event.ClientId.ToString())
                .SendAsync("newNotification", notf);
        }
    }
}
