namespace FreelancerProfile.Application.Queries
{
    public interface IFreelancerReadModelRepository
    {
        Task<FreelancerViewModel> GetByIdAsync(Guid id);
        Task<FreelancerViewModel> GetByUserIdAsync(Guid userId);
        Task CreateAsync(FreelancerViewModel freelancer);
        Task UpdateAsync(FreelancerViewModel freelancer);
    }
}
