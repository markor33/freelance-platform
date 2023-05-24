using EventBus.Abstractions;
using System.Reflection;

namespace EventBus.Extensions
{
    public static class EventBusExtensions
    {
        public static void AddHandlers(this IEventBus eventBus, Assembly assembly)
        {
            var handlerTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>)))
                .ToList();

            foreach (var handlerType in handlerTypes)
            {
                var eventType = handlerType.GetInterfaces()
                                           .First(i => i.IsGenericType &&
                                                       i.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>))
                                           .GetGenericArguments()[0];

                var subscribeMethod = typeof(IEventBus)
                                      .GetMethod("Subscribe")
                                      .MakeGenericMethod(eventType, handlerType);

                subscribeMethod.Invoke(eventBus, null);
            }
        }
    }
}
