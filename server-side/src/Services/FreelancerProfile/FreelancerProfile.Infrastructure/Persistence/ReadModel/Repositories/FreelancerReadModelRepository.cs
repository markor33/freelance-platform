using FreelancerProfile.Application.Queries;
using FreelancerProfile.Infrastructure.Persistence.ReadModel.Settings;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace FreelancerProfile.Infrastructure.Persistence.ReadModel.Repositories
{
    public class FreelancerReadModelRepository : IFreelancerReadModelRepository
    {
        private readonly IMongoCollection<FreelancerViewModel> _collection;

        public FreelancerReadModelRepository(IMongoDbFactory mongoDbFactory)
        {
            _collection = mongoDbFactory.GetCollection<FreelancerViewModel>();
        }

        public async Task<FreelancerViewModel> GetByIdAsync(Guid id)
        {
            var filter = Builders<FreelancerViewModel>.Filter.Eq(fr => fr.Id, id);
            return await (await _collection.FindAsync(filter)).FirstOrDefaultAsync();
        }

        public async Task<FreelancerViewModel> GetByUserIdAsync(Guid userId)
        {
            var filter = Builders<FreelancerViewModel>.Filter.Eq(fr => fr.UserId, userId);
            return await (await _collection.FindAsync(filter)).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(FreelancerViewModel freelancer)
        {
            await _collection.InsertOneAsync(freelancer);
        }

        public async Task UpdateAsync(FreelancerViewModel freelancer)
        {
            var filter = Builders<FreelancerViewModel>.Filter.Eq(fr => fr.Id, freelancer.Id);
            await _collection.ReplaceOneAsync(filter, freelancer);
        }

        public async Task UpdateAsync<TField>(Guid id, Expression<Func<FreelancerViewModel, TField>> field, TField value)
        {
            var filter = Builders<FreelancerViewModel>.Filter.Eq(fr => fr.Id, id);
            var update = Builders<FreelancerViewModel>.Update.Set(field, value);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task AddToNestedListAsync<TItem>(Guid id, Expression<Func<FreelancerViewModel, IEnumerable<TItem>>> field, TItem value)
        {
            var filter = Builders<FreelancerViewModel>.Filter.Eq(fr => fr.Id, id);
            var update = Builders<FreelancerViewModel>.Update.Push(field, value);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateNestedListItemAsync<TItem>(Guid id, Expression<Func<FreelancerViewModel, IEnumerable<TItem>>> field, Guid valueId, TItem value)
        {
            var fieldPath = GetMemberName(field.Body);

            var filter = Builders<FreelancerViewModel>.Filter.Eq(fr => fr.Id, id) &
                 Builders<FreelancerViewModel>.Filter.Eq($"{fieldPath}.Id", valueId);
            var update = Builders<FreelancerViewModel>.Update.Set($"{fieldPath}.$", value);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task RemoveFromNestedListAsync<TItem>(Guid id, Expression<Func<FreelancerViewModel, IEnumerable<TItem>>> field, Guid nestedItemId)
        {
            var documentFilter = Builders<FreelancerViewModel>.Filter.Eq(fr => fr.Id, id);
            var nestedItemFilter = Builders<TItem>.Filter.Eq("Id", nestedItemId);
            var update = Builders<FreelancerViewModel>.Update.PullFilter(field, nestedItemFilter);

            await _collection.UpdateOneAsync(documentFilter, update);
        }

        private string GetMemberName(Expression expression)
        {
            switch (expression)
            {
                case MemberExpression m:
                    var memberName = GetMemberName(m.Expression);
                    return string.IsNullOrEmpty(memberName) ? m.Member.Name : $"{memberName}.{m.Member.Name}";
                case UnaryExpression u when u.Operand is MemberExpression m:
                    return GetMemberName(m);
                default:
                    return string.Empty;
            }
        }

    }
}
