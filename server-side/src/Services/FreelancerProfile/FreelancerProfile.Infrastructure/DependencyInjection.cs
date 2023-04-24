using FreelancerProfile.Application.Queries;
using FreelancerProfile.Application.Services;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Infrastructure.ReadModel.Repositories;
using FreelancerProfile.Infrastructure.ReadModel.Settings;
using FreelancerProfile.Infrastructure.Repositories;
using FreelancerProfile.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ProfileManagemenet.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient(typeof(IFreelancerRepository), typeof(FreelancerRepository));

            services.AddScoped(typeof(IMongoDbFactory), typeof(MongoDbFactory));
            services.AddTransient(typeof(IFreelancerReadModelRepository), typeof(FreelancerReadModelRepository));

            services.AddTransient(typeof(ILanguageService), typeof(LanguageService));
            services.AddTransient(typeof(IProfessionService), typeof(ProfessionService));
            services.AddTransient(typeof(ISkillService), typeof(SkillService));

            return services;
        }
    }
}
