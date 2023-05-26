using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class EducationEdittedDomainEventHandler : INotificationHandler<EducationEdittedDomainEvent>
    {
        private readonly IFreelancerReadModelRepository _repository;
        private readonly IMapper _mapper;

        public EducationEdittedDomainEventHandler(IFreelancerReadModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(EducationEdittedDomainEvent notification, CancellationToken cancellationToken)
        {
            var freelancer = await _repository.GetByIdAsync(notification.FreelancerId);

            freelancer.UpdateEducation(_mapper.Map<EducationViewModel>(notification.Education));

            await _repository.UpdateAsync(freelancer);
        }
    }
}
