using FreelancerProfile.Domain.SeedWork;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate
{
    public interface IFreelancerRepository : IRepository<Freelancer>
    {
        Task<Freelancer> GetByUserId(Guid userId);
        Task<Freelancer> CreateAsync(Freelancer freelancer);
    }
}
