using EventBus.Abstractions;
using FeedbackManagement.API.IntegrationEvents.Events;
using FeedbackManagement.API.Models;
using FeedbackManagement.API.Persistence;

namespace FeedbackManagement.API.IntegrationEvents.Handlers
{
    public class ContractFinishedIntegrationEventHandler : IIntegrationEventHandler<ContractFinishedIntegrationEvent>
    {
        private readonly IFinishedContractRepository _finishedContractRepository;

        public ContractFinishedIntegrationEventHandler(IFinishedContractRepository finishedContractRepository)
        {
            _finishedContractRepository = finishedContractRepository;
        }

        public async Task HandleAsync(ContractFinishedIntegrationEvent @event)
        {
            var finishedContract = new FinishedContract(@event.ContractId, @event.JobId, @event.ClientId, @event.FreelancerId);
            await _finishedContractRepository.Create(finishedContract);
        }
    }
}
