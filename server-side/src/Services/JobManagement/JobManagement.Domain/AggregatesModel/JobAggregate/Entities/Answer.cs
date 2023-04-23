using FreelancerProfile.Domain.SeedWork;
using System.Text.Json.Serialization;

namespace JobManagement.Domain.AggregatesModel.JobAggregate.Entities
{
    public class Answer : Entity<Guid>
    {
        public Guid QuestionId { get; private set; }
        public string Text { get; private set; }

        public Answer() { }

        [JsonConstructor]
        public Answer(Guid questionId, string text)
        {
            QuestionId = questionId;
            Text = text;
        }

    }
}
