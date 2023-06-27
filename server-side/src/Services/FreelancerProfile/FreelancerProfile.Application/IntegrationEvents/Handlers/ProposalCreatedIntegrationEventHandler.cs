using EventBus.Abstractions;
using FreelancerProfile.Application.Commands;
using FreelancerProfile.Application.IntegrationEvents.Events;
using MediatR;

namespace FreelancerProfile.Application.IntegrationEvents.Handlers
{
    public class ProposalCreatedIntegrationEventHandler : IIntegrationEventHandler<ProposalCreatedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public ProposalCreatedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task HandleAsync(ProposalCreatedIntegrationEvent @event)
        {
            var command = new SubtractCreditsCommand(@event.FreelancerId, @event.JobId, @event.ProposalId, @event.PriceInCredits);
            await _mediator.Send(command);
        }

    }
}
