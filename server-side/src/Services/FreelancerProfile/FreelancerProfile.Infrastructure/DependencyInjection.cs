using FreelancerProfile.Application.Queries;
using FreelancerProfile.Application.Services;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Infrastructure.Persistence.ReadModel.Repositories;
using FreelancerProfile.Infrastructure.Persistence.ReadModel.Settings;
using FreelancerProfile.Infrastructure.Persistence.Repositories;
using FreelancerProfile.Infrastructure.Persistence.Services;
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

            services.AddScoped(typeof(IFileUploader), typeof(AzureBlobStorageService));

            return services;
        }
    }
}
