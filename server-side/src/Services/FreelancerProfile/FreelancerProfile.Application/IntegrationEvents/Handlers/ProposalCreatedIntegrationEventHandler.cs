using EventBus.Abstractions;
using FreelancerProfile.Application.IntegrationEvents.Events;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;

namespace FreelancerProfile.Application.IntegrationEvents.Handlers
{
    public class ProposalCreatedIntegrationEventHandler : IIntegrationEventHandler<ProposalCreatedIntegrationEvent>
    {
        private readonly IFreelancerRepository _freelancerRepository;
        private readonly IEventBus _eventBus;

        public ProposalCreatedIntegrationEventHandler(IFreelancerRepository freelancerRepository, IEventBus eventBus)
        {
            _freelancerRepository = freelancerRepository;
            _eventBus = eventBus;
        }

        
        public async Task HandleAsync(ProposalCreatedIntegrationEvent @event)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(@event.FreelancerId);

            var result = freelancer.SubtractCredits(@event.PriceInCredits);

            if (!result)
                _eventBus.Publish(new CreditsLimitExceededIntegrationEvent(@event.JobId, @event.ProposalId));
            _eventBus.Publish(new CreditsReservedIntegrationEvent(@event.JobId, @event.ProposalId));

            await _freelancerRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
