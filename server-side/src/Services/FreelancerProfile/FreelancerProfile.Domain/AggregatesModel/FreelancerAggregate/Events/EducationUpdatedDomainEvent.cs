using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class EducationUpdatedDomainEvent : INotification
    {
        public Guid FreelancerId { get; private set; }
        public Education Education { get; private set; }

        public EducationUpdatedDomainEvent(Guid freelancerId, Education education)
        {
            FreelancerId = freelancerId;
            Education = education;
        }

    }
}
