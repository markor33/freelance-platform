using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;

namespace JobManagement.Domain.Repositories
{
    public interface ISkillRepository
    {
        Task<Skill> GetByIdAsync(Guid id);
        Task<List<Skill>> GetByIdsAsync(List<Guid> ids);
    }
}
