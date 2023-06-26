using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntegrationEventLog.EFCore.Services
{
    public class IntegrationEventSenderService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;
        private readonly Dictionary<string, Type> _eventTypes = new Dictionary<string, Type>();

        public IntegrationEventSenderService(IServiceProvider serviceProvider, List<IntegrationEvent> integrationEvents)
        {
            _serviceProvider = serviceProvider;
            foreach (var integrationEvent in integrationEvents)
            {
                _eventTypes.Add(integrationEvent.GetType().Name, integrationEvent.GetType());
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ProcessOutbox, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private async void ProcessOutbox(object state)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IntegrationEventLogContext>();
            var integrationEventLogService = new IntegrationEventLogService(context.Database.GetDbConnection());
            var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

            var eventsToPublish = await context.IntegrationEventLogs.Where(e => e.State == EventState.NotPublished).ToListAsync();

            foreach (var eventToPublish in eventsToPublish)
            {
                _eventTypes.TryGetValue(eventToPublish.EvenTypeShortName, out Type eventType);
                eventToPublish.DeserializeJsonContent(eventType);
                try
                {
                    await integrationEventLogService.MarkEventAsInProgressAsync(eventToPublish.EventId);
                    eventBus.Publish(eventToPublish.IntegrationEvent);
                    await integrationEventLogService.MarkEventAsPublishedAsync(eventToPublish.EventId);
                }
                catch (Exception)
                {
                    await integrationEventLogService.MarkEventAsFailedAsync(eventToPublish.EventId);
                }
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
