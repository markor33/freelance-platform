using Elastic.Clients.Elasticsearch;
using JobSearch.Abstractions;
using JobSearch.Elastic.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobSearch.Elastic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddElastic(this IServiceCollection services, IConfiguration configuration)
        {
            var client = new ElasticsearchClient(new Uri(configuration.GetConnectionString("elasticsearch")));
            // JobMapping.Create(client);
            services.AddSingleton<ElasticsearchClient>(client);

            services.AddScoped(typeof(IJobRepository), typeof(JobRepository));

            return services;
        }
    }
}
