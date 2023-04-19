using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using FreelancerProfile.Domain.SeedWork;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities
{
    public class Employment : Entity<Guid>
    {
        public string Company { get; private set; }
        public string Title { get; private set; }
        public DateRange Period { get; private set; }
        public string Description { get; private set; }

        public Employment() { }

        public Employment(string company, string title, DateRange period, string description)
        {
            Company = company;
            Title = title;
            Period = period;
            Description = description;
        }

    }
}
