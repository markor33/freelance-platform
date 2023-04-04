using FreelancerProfile.Domain.SeedWork;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate
{
    public class Freelancer : Entity<Guid>, IAggregateRoot
    {
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set;}
        public Contact Contact { get; private set; }

        public Freelancer() { }

        public Freelancer(Guid userId, string firstName, string lastName, Contact contact)
        {
            Id = new Guid();
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Contact = contact;
        }

    }
}
