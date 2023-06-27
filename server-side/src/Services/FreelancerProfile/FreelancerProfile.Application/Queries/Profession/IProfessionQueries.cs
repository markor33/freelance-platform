using FluentResults;

namespace FreelancerProfile.Application.Queries
{
    public interface IProfessionQueries
    {
        Task<List<ProfessionViewModel>> GetAllAsync();
        Task<Result<ProfessionViewModel>> GetByIdAsync(Guid id);
        Task<Result<SkillViewModel>> GetSkillByIdAsync(Guid id);
        Task<List<SkillViewModel>> GetSkillsByProfessionAsync(Guid id);
    }
}
