using MediatR;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class FreelancerCreatedDomainEvent : INotification
    {
        public Freelancer Freelancer { get; private set; }

        public FreelancerCreatedDomainEvent(Freelancer freelancer)
        {
            Freelancer = freelancer;
        }

    }
}
