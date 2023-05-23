using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbackManagement.API.Models
{
    public class FinishedContract
    {
        [Key]
        public Guid Id { get; private set; }
        public Guid JobId { get; private set; }
        public Guid ClientId { get; private set; }
        [Column(TypeName = "jsonb")]
        public Feedback? ClientFeedback { get; private set; }
        public Guid FreelancerId { get; private set; }
        [Column(TypeName = "jsonb")]
        public Feedback? FreelancerFeedback { get; private set; }

        public FinishedContract() { }

        public FinishedContract(Guid id, Guid jobId, Guid clientId, Guid freelancerId)
        {
            Id = id;
            JobId = jobId;
            ClientId = clientId;
            FreelancerId = freelancerId;
        }

        public void SetClientFeedback(Feedback clientFeedback)
        {
            ClientFeedback = clientFeedback;
        }

        public void SetFreelancerFeedback(Feedback freelancerFeedback)
        {
            FreelancerFeedback = freelancerFeedback;
        }

    }
}
