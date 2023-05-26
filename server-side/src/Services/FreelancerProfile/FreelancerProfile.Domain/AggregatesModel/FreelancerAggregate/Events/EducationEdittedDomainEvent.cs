using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using MediatR;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class EducationEdittedDomainEvent : INotification
    {
        public Guid FreelancerId { get; private set; }
        public Education Education { get; private set; }

        public EducationEdittedDomainEvent(Guid freelancerId, Education education)
        {
            FreelancerId = freelancerId;
            Education = education;
        }

    }
}
