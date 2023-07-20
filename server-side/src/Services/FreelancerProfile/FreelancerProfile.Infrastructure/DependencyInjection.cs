﻿using FreelancerProfile.Application.IntegrationEvents;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Application.Services;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Infrastructure.Persistence.EventStore;
using FreelancerProfile.Infrastructure.Persistence.LoadingStrategy;
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
            services.AddScoped(typeof(IFreelancerRepository), typeof(FreelancerRepository));
            services.AddScoped<ILoadingStrategyFactory, LoadingStrategyFactory>();
            services.AddScoped<StandardLoadingStrategy>();
            services.AddScoped<EventSourcingLoadingStrategy>();

            services.AddScoped(typeof(IEventStore), typeof(EventStore));

            services.AddScoped(typeof(IMongoDbFactory), typeof(MongoDbFactory));
            services.AddTransient(typeof(IFreelancerReadModelRepository), typeof(FreelancerReadModelRepository));

            services.AddScoped(typeof(IFreelancerProfileIntegrationEventService), typeof(FreelancerProfileIntegrationEventService));
            services.AddTransient(typeof(ILanguageService), typeof(LanguageService));
            services.AddTransient(typeof(IProfessionService), typeof(ProfessionService));
            services.AddTransient(typeof(ISkillService), typeof(SkillService));

            services.AddScoped(typeof(IFileUploader), typeof(AzureBlobStorageService));

            return services;
        }
    }
}
