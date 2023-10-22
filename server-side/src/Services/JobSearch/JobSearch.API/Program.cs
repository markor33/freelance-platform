using EventBus.Abstractions;
using EventBus;
using EventBusRabbitMQ;
using EventBus.Extensions;
using RabbitMQ.Client;
using JobSearch.API.IntegrationEvents.Handlers;
using JobSearch.Elastic;

namespace JobSearch.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            builder.Services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "host.docker.internal",
                    Port = 60000,
                    UserName = "guest",
                    Password = "guest",
                };

                return new DefaultRabbitMQPersistentConnection(factory, 5);
            });
            builder.Services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = "jobsearch";
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, eventBusSubcriptionsManager, sp, 5, subscriptionClientName);
            });

            builder.Services.AddIntegrationEventsHandlers(typeof(JobCreatedIntegrationEventHandler).Assembly);

            builder.Services.AddElastic(builder.Configuration);

            var app = builder.Build();

            var eventBus = app.Services.GetRequiredService<IEventBus>();
            eventBus.AddHandlers(typeof(JobCreatedIntegrationEventHandler).Assembly);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}