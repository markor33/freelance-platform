using GrpcClientProfile;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JobManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient(typeof(IJobRepository), typeof(JobRepository));

            // grpc
            services.AddGrpcClient<ClientProfileGrpc.ClientProfileGrpcClient>((services, options) =>
            {

            });

            return services;
        }
    }
}
