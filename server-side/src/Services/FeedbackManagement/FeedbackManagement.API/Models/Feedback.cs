using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FeedbackManagement.API.Models
{
    public class Feedback
    {
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; private set; }
        [MinLength(10, ErrorMessage = "Feedback text must be at least 10 characters long.")]
        [MaxLength(500, ErrorMessage = "Feedback text cannot exceed 500 characters.")]
        public string Text { get; private set; }

        public Feedback() { }

        [JsonConstructor]
        public Feedback(int rating, string text)
        {
            Rating = rating;
            Text = text;
        }

    }
}
