using GrpcFeedbackManagement;

namespace Web.Bff.Models
{
    public class Feedback
    {
        public Guid JobId { get; set; }
        public string JobTitle { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }

        public Feedback(FeedbackDTO feedbackDTO, string jobTitle)
        {
            JobId = Guid.Parse(feedbackDTO.JobId);
            JobTitle = jobTitle;
            Rating = feedbackDTO.Rating;
            Text = feedbackDTO.Text;
        }
    }
}
