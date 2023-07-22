using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class FreelancerCreatedDomainEventHandler : INotificationHandler<FreelancerCreated>
    {
        private readonly IFreelancerReadModelRepository _repository;

        public FreelancerCreatedDomainEventHandler(IFreelancerReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(FreelancerCreated notification, CancellationToken cancellationToken)
        {
            var freelancerViewModel = new FreelancerViewModel(notification.AggregateId, notification.UserId, 
                notification.FirstName, notification.LastName, notification.Contact);
            await _repository.CreateAsync(freelancerViewModel);
        }
    }
}
