using System.Linq.Expressions;

namespace FreelancerProfile.Application.Queries
{
    public interface IFreelancerReadModelRepository
    {
        Task<FreelancerViewModel> GetByIdAsync(Guid id);
        Task<FreelancerViewModel> GetByUserIdAsync(Guid userId);
        Task CreateAsync(FreelancerViewModel freelancer);
        Task UpdateAsync(FreelancerViewModel freelancer);
        Task UpdateAsync<TField>(Guid id, Expression<Func<FreelancerViewModel, TField>> field, TField value);
        Task AddToNestedListAsync<TItem>(Guid id, Expression<Func<FreelancerViewModel, IEnumerable<TItem>>> field, TItem value);
        Task UpdateNestedListItemAsync<TItem>(Guid id, Expression<Func<FreelancerViewModel, IEnumerable<TItem>>> field, Guid valueId, TItem value);
        Task RemoveFromNestedListAsync<TItem>(Guid id, Expression<Func<FreelancerViewModel, IEnumerable<TItem>>> field, Guid nestedItemId);
    }
}
