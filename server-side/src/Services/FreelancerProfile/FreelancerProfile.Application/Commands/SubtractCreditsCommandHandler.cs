using FreelancerProfile.Application.IntegrationEvents;
using FreelancerProfile.Application.IntegrationEvents.Events;
using FreelancerProfile.Domain.Repositories;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class SubtractCreditsCommandHandler : IRequestHandler<SubtractCreditsCommand, Unit>
    {
        private readonly IFreelancerRepository _freelancerRepository;
        private readonly IFreelancerProfileIntegrationEventService _integrationEventService;

        public SubtractCreditsCommandHandler(
            IFreelancerRepository freelancerRepository, 
            IFreelancerProfileIntegrationEventService integrationEventService)
        {
            _freelancerRepository = freelancerRepository;
            _integrationEventService = integrationEventService;
        }

        public async Task<Unit> Handle(SubtractCreditsCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(request.FreelancerId);

            var result = freelancer.SubtractCredits(request.Credits);
            if (!result)
                await _integrationEventService.SaveEventAsync(new CreditsLimitExceededIntegrationEvent(request.JobId, request.ProposalId));
            else
                await _integrationEventService.SaveEventAsync(new CreditsReservedIntegrationEvent(request.JobId, request.ProposalId));

            await _freelancerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
