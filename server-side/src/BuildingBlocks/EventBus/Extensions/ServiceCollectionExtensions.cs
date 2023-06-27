using EventBus.Abstractions;
using EventBus.Events;
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

        public static void AddIntegrationEventsList(this IServiceCollection serviceCollection, Assembly assembly)
        {
            var eventInstances = assembly.GetTypes()
                .Where(t => t.BaseType == typeof(IntegrationEvent) && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IntegrationEvent>()
                .ToList();

            serviceCollection.AddSingleton(eventInstances);
        }
    }
}
