using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;

namespace FreelancerProfile.Application.Services
{
    public interface IProfessionService
    {
        Task<Profession> GetByIdAsync(Guid id);
    }
}
