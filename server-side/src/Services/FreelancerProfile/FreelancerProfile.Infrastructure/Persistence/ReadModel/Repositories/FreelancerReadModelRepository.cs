using FreelancerProfile.Application.Queries;
using FreelancerProfile.Infrastructure.Persistence.ReadModel.Settings;
using MongoDB.Driver;

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

    }
}
