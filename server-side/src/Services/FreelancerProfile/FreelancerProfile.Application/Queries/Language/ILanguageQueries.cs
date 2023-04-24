using FluentResults;

namespace FreelancerProfile.Application.Queries
{
    public interface ILanguageQueries
    {
        Task<List<LanguageViewModel>> GetAllAsync();
        Task<Result<LanguageViewModel>> GetByIdAsync(int id);
    };
}
