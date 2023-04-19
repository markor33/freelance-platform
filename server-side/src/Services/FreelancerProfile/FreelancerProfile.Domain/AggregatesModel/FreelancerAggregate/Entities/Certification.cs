using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using FreelancerProfile.Domain.SeedWork;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities
{
    public class Certification : Entity<Guid>
    {
        public string Name { get; private set; }
        public string Provider { get; private set; }
        public DateRange Attended { get; private set; }
        public string Description { get; private set; }

        public Certification() { }

        public Certification(string name, string provider, DateRange attended, string description)
        {
            Name = name;
            Provider = provider;
            Attended = attended;
            Description = description;
        }

    }
}
