using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entites;

namespace FreelancerProfile.Application.Queries
{
    public interface IProfessionQueries
    {
        Task<Profession> GetByIdAsync(Guid id);
    }
}
