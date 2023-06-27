using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using FreelancerProfile.Domain.SeedWork;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities
{
    public class Education : Entity<Guid>
    {
        public string SchoolName { get; private set; }
        public string Degree { get; private set; }
        public DateRange Attended { get; private set; }

        public Education()
        {

        }

        public Education(string schoolName, string degree, DateRange attended)
        {
            SchoolName = schoolName;
            Degree = degree;
            Attended = attended;
        }

        public void Update(string schoolName, string degree, DateRange attended)
        {
            SchoolName = schoolName;
            Degree = degree;
            Attended = attended;
        }
    }
}
