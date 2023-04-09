using FreelancerProfile.Domain.SeedWork;
using System.Text.Json.Serialization;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects
{
    public class DateRange : ValueObject
    {
        public DateOnly Start { get; private set; }
        public DateOnly End { get; private set; }

        public DateRange() { }

        [JsonConstructor]
        public DateRange(DateOnly start, DateOnly end)
        {
            Start = start;
            End = end;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }
    }
}
