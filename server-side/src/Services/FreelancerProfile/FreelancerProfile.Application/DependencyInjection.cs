﻿using FluentValidation;
using FreelancerProfile.Application.Behaviors;
using FreelancerProfile.Application.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FreelancerProfile.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddTransient(typeof(IFreelancerQueries), typeof(FreelancerQueries));
            services.AddTransient(typeof(ILanguageQueries), typeof(LanguageQueries));
            services.AddTransient(typeof(IProfessionQueries), typeof(ProfessionQueries));

            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
