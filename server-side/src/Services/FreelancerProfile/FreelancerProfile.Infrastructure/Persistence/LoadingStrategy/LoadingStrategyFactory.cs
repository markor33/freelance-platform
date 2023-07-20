using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FreelancerProfile.Infrastructure.Persistence.LoadingStrategy
{
    public interface ILoadingStrategyFactory
    {
        IAggregateLoadingStrategy CreateLoadingStrategy();
    }

    public class LoadingStrategyFactory : ILoadingStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly LoadingStrategySettings _settings;

        public LoadingStrategyFactory(IServiceProvider serviceProvider, IOptions<LoadingStrategySettings> settings)
        {
            _serviceProvider = serviceProvider;
            _settings = settings.Value;
        }

        public IAggregateLoadingStrategy CreateLoadingStrategy()
        {
            if (_settings.UseEventSourcing)
            {
                return _serviceProvider.GetRequiredService<EventSourcingLoadingStrategy>();
            }
            else
            {
                return _serviceProvider.GetRequiredService<StandardLoadingStrategy>();
            }
        }
    }
}
