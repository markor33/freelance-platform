using FeedbackManagement.API.Models;

namespace FeedbackManagement.API.Persistence
{
    public interface IFinishedContractRepository
    {
        Task<FinishedContract> GetById(Guid id);
        Task<List<FinishedContract>> GetByFreelancer(Guid freelancerId);
        Task<float> GetFreelancerAverageRating(Guid freelancerId);
        Task<float> GetClientAverageRating(Guid clientId);
        Task Create(FinishedContract finishedContract);
        Task Save();
    }
}
