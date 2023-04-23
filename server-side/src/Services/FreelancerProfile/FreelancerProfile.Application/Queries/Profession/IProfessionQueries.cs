using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;

namespace FreelancerProfile.Application.Queries
{
    public interface IProfessionQueries
    {
        Task<Profession> GetByIdAsync(Guid id);
    }
}
