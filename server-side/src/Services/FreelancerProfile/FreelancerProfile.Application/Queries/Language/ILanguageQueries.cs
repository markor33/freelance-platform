using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entites;

namespace FreelancerProfile.Application.Queries
{
    public interface ILanguageQueries
    {
        Task<Language> GetByIdAsync(int id);
    };
}
