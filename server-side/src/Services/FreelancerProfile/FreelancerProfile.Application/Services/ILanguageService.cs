using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;

namespace FreelancerProfile.Application.Services
{
    public interface ILanguageService
    {
        Task<Language> GetByIdAsync(int id);
    }
}
