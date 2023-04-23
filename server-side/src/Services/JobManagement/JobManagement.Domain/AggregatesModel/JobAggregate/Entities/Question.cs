using FreelancerProfile.Domain.SeedWork;
using System.Text.Json.Serialization;

namespace JobManagement.Domain.AggregatesModel.JobAggregate.Entities
{
    public class Question : Entity<Guid>
    {
        public string Text { get; private set; }

        public Question() { }

        [JsonConstructor]
        public Question(string text)
        {
            Id = Guid.NewGuid();
            Text = text;
        }
    }
}
