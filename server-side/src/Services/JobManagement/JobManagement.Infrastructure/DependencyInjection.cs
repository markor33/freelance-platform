using JobManagement.Infrastructure.LoadingStrategy;
using JobManagement.Application.IntegrationEvents;
using JobManagement.Infrastructure.Persistence.Services;
using JobManagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using JobManagement.Infrastructure.EventStore;
using JobManagement.Domain.Repositories;

namespace JobManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped(typeof(IJobRepository), typeof(JobRepository));
            services.AddScoped(typeof(IProfessionRepository), typeof(ProfessionRepository));
            services.AddScoped(typeof(ISkillRepository), typeof(SkillRepository));

            services.AddScoped<ILoadingStrategyFactory, LoadingStrategyFactory>();
            services.AddScoped<StandardLoadingStrategy>();
            services.AddScoped<EventSourcingLoadingStrategy>();

            services.AddScoped(typeof(IEventStore), typeof(EventStore.EventStore));

            services.AddScoped(typeof(IJobIntegrationEventService), typeof(JobIntegrationEventService));

            return services;
        }
    }
}
