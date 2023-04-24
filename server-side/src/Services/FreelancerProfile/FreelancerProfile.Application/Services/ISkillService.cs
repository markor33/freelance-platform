using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;

namespace FreelancerProfile.Application.Services
{
    public interface ISkillService
    {
        Task<Skill> GetByIdAsync(Guid id);
    }
}
