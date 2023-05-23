using GrpcFeedbackManagement;

namespace Web.Bff.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly Feedback.FeedbackClient _feedbackClient;
        private readonly IJobManagementService _jobManagementService;

        public FeedbackService(Feedback.FeedbackClient feedbackClient, IJobManagementService jobManagementService)
        {
            _feedbackClient = feedbackClient;
            _jobManagementService = jobManagementService;
        }

        public async Task<List<Models.Feedback>> GetByFreelancer(string freelancerId)
        {
            var request = new GetFreelancerFeedbackRequest() { FreelancerId = freelancerId };
            var response = await _feedbackClient.GetFreelancerFeedbackAsync(request);
            var feedbacks = new List<Models.Feedback>();
            foreach (var feedbackDTO in response.Feedbacks)
            {
                var job = await _jobManagementService.GetById(feedbackDTO.JobId);
                var feedback = new Models.Feedback(feedbackDTO, job.Title);
                feedbacks.Add(feedback);
            }
            return feedbacks;
        }

        public async Task<float> GetClientAverageRating(string clientId)
        {
            var request = new GetAverageClientRatingRequest() { ClientId = clientId };
            return (await _feedbackClient.GetAverageClientRatingAsync(request)).AverageRating;
        }

        public async Task<float> GetFreelancerAverageRating(string freelancerId)
        {
            var request = new GetAverageFreelancerRatingRequest() { FreelancerId = freelancerId };
            return (await _feedbackClient.GetAverageFreelancerRatingAsync(request)).AverageRating; 
        }
    }
}
