using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class EducationAddedDomainEvent : INotification
    {
        public Guid FreelancerId { get; private set; }
        public Education Education { get; private set; }

        public EducationAddedDomainEvent(Guid freelancerId, Education education)
        {
            FreelancerId = freelancerId;
            Education = education;
        }
    }
}
