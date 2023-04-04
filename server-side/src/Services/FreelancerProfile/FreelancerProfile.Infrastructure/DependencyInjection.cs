using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ProfileManagemenet.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient(typeof(IFreelancerRepository), typeof(FreelancerRepository));
            return services;
        }
    }
}
