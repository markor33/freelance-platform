using FreelancerProfile.Domain.SeedWork;

namespace FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate
{
    public interface IFreelancerRepository : IRepository<Freelancer>
    {
        Task<Freelancer> GetByIdAsync(Guid id);
        Task<Freelancer> CreateAsync(Freelancer freelancer);
    }
}
