using Elastic.Clients.Elasticsearch;
using JobSearch.Abstractions;
using JobSearch.Abstractions.Model;

namespace JobSearch.Elastic
{
    public class JobRepository : IJobRepository
    {
        private static readonly string INDEX_NAME = "job";
        private readonly ElasticsearchClient _client;

        public JobRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task CreateAsync(Job job)
        {
            await _client.IndexAsync<Job>(job, INDEX_NAME);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _client.DeleteAsync(INDEX_NAME, id);
        }

        public async Task UpdateAsync(Job job)
        {
            await _client.UpdateAsync<Job, Job>(INDEX_NAME, job.Id, u => u.Doc(job));
        }
    }
}
