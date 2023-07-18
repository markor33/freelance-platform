using ClientProfile.API.Infrastructure.Repositories;
using ClientProfile.API.IntegrationEvents.Events;
using ClientProfile.API.Model;
using EventBus.Abstractions;

namespace ClientProfile.API.IntegrationEvents.Handlers
{
    public class ClientRegisteredIntegrationEventHandler : IIntegrationEventHandler<ClientRegisteredIntegrationEvent>
    {
        private readonly IClientRepository _clientRepository;

        public ClientRegisteredIntegrationEventHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task HandleAsync(ClientRegisteredIntegrationEvent @event)
        {
            var client = new Client(@event.UserId, @event.FirstName, @event.LastName, @event.Contact);
            await _clientRepository.CreateAsync(client);
        }
    }
}
