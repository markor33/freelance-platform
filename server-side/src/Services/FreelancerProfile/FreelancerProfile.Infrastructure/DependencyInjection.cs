using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Infrastructure.Queries;
using FreelancerProfile.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ProfileManagemenet.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient(typeof(IFreelancerRepository), typeof(FreelancerRepository));

            // queries
            services.AddTransient(typeof(IFreelancerQueries), typeof(FreelancerQueries));
            services.AddTransient(typeof(ILanguageQueries), typeof(LanguageQueries));
            services.AddTransient(typeof(IProfessionQueries), typeof(ProfessionQueries));
            services.AddTransient(typeof(ISkillQueries), typeof(SkillQueries));

            return services;
        }
    }
}
