using Web.Bff.Models;

namespace Web.Bff.Services
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> GetByFreelancer(string freelancerId);
        Task<float> GetFreelancerAverageRating(string freelancerId);
        Task<float> GetClientAverageRating(string clientId);
    }
}
