﻿using JobManagement.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using JobManagement.Application.Queries;

namespace JobManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

            services.AddTransient(typeof(IJobQueries), typeof(JobQueries));
            services.AddTransient(typeof(IProposalQueries), typeof(ProposalQueries));
            services.AddTransient(typeof(IContractQueries), typeof(ContractQueries));
            services.AddTransient(typeof(IAnswerQueries), typeof(AnswerQueries));

            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
