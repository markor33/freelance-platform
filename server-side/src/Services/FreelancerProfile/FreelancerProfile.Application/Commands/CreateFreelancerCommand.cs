using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class CreateFreelancerCommand : IRequest<bool>
    {
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set;}
        public Contact Contact { get; private set; }

        public CreateFreelancerCommand(Guid userId, string firstName, string lastName, Contact contact)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Contact = contact;
        }
    }
}
