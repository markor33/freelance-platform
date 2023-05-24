using EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventBus.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIntegrationEventsHandlers(this IServiceCollection serviceCollection, Assembly assembly)
        {
            var handlerTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>)))
                .ToList();

            foreach (var handlerType in handlerTypes)
                serviceCollection.AddScoped(handlerType);
        }
    }
}
