using JobManagement.Application.IntegrationEvents;
using JobManagement.Application.Services;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Infrastructure.Persistence.Services;
using JobManagement.Infrastructure.Repositories;
using JobManagement.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JobManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient(typeof(IJobRepository), typeof(JobRepository));

            services.AddScoped(typeof(IJobIntegrationEventService), typeof(JobIntegrationEventService));
            services.AddTransient(typeof(IProfessionService), typeof(ProfessionService));
            services.AddTransient(typeof(ISkillService), typeof(SkillService));

            return services;
        }
    }
}
