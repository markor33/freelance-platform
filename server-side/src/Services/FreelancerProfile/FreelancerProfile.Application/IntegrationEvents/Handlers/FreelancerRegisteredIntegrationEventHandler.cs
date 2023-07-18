using EventBus.Abstractions;
using FreelancerProfile.Application.Commands;
using FreelancerProfile.Application.IntegrationEvents.Events;
using MediatR;

namespace FreelancerProfile.Application.IntegrationEvents.Handlers
{
    public class FreelancerRegisteredIntegrationEventHandler : IIntegrationEventHandler<FreelancerRegisteredIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public FreelancerRegisteredIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task HandleAsync(FreelancerRegisteredIntegrationEvent @event)
        {
            var command = new CreateFreelancerCommand(@event.UserId, @event.LastName, @event.LastName, @event.Contact);
            await _mediator.Send(command);
        }
    }
}
