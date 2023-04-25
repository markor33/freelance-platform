using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;

namespace JobManagement.Application.Services
{
    public interface ISkillService
    {
        Task<Skill> GetByIdAsync(Guid id);
    }
}
