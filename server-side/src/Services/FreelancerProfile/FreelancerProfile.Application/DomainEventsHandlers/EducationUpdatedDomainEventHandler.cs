using AutoMapper;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events;
using MediatR;

namespace FreelancerProfile.Application.DomainEventsHandlers
{
    public class EducationUpdatedDomainEventHandler : INotificationHandler<EducationUpdated>
    {
        private readonly IFreelancerReadModelRepository _repository;

        public EducationUpdatedDomainEventHandler(IFreelancerReadModelRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(EducationUpdated notification, CancellationToken cancellationToken)
        {
            var education = new EducationViewModel(notification.EducationId, notification.SchoolName, notification.Degree, notification.Attended);
            await _repository.UpdateNestedListItemAsync(notification.AggregateId, fr => fr.Educations, education.Id, education);
        }
    }
}
