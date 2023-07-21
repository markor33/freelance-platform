using JobManagement.Domain.SeedWork;
using System.Text.Json.Serialization;

namespace JobManagement.Domain.AggregatesModel.JobAggregate.Entities
{
    public class Question : Entity<Guid>
    {
        public string Text { get; private set; }

        public Question() { }

        [JsonConstructor]
        public Question(Guid id, string text)
        {
            Id = id;
            Text = text;
        }

        public void SetText(string text) => Text = text;

    }
}
