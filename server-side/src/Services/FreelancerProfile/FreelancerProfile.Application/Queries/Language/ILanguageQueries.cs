using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;

namespace FreelancerProfile.Application.Queries
{
    public interface ILanguageQueries
    {
        Task<Language> GetByIdAsync(int id);
    };
}
