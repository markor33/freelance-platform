using MediatR;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Events
{
    public class ProfileSetupCompletedDomainEvent : INotification
    {
        public Freelancer Freelancer { get; private set; }

        public ProfileSetupCompletedDomainEvent(Freelancer freelancer)
        {
            Freelancer = freelancer;
        }
    }
}
