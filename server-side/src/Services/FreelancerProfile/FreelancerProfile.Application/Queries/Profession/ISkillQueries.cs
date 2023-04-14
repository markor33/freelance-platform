using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entites;

namespace FreelancerProfile.Application.Queries
{
    public interface ISkillQueries
    {
        Task<Skill> GetByIdAsync(Guid id);
        Task<List<Skill>> GetByProfession(Guid professionId);
    }
}
