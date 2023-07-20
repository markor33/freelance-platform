using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class ProfileSetupCompletedDomainEventHandler : INotificationHandler<ProfileSetupCompleted>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public ProfileSetupCompletedDomainEventHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(ProfileSetupCompleted notification, CancellationToken cancellationToken)
        {
            var freelancer = await _repository.GetByIdAsync(notification.AggregateId);
            var profession = _mapper.Map<ProfessionViewModel>(notification.Profession);

            freelancer.ProfileSetup(notification.ProfileSummary, notification.HourlyRate, 
                notification.Availability, notification.ExperienceLevel, profession, notification.LanguageKnowledge);

            await _repository.UpdateAsync(freelancer);
        }
    }
}
