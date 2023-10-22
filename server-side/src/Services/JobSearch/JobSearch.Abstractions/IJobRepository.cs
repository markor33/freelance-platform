using JobSearch.Abstractions.Model;

namespace JobSearch.Abstractions
{
    public interface IJobRepository
    {
        Task CreateAsync(Job job);
        Task UpdateAsync(Job job);
        Task DeleteAsync(Guid id);
    }
}
