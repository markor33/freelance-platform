using EventBus.Abstractions;
using EventBus;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NotifyChat.SignalR.Persistence.Settings;
using Moq;
using MongoDB.Driver;
using NotifyChat.SignalR.Models;

namespace NotiftChat.IntegrationTests.Setup
{
    public class TestDatabaseFactory : WebApplicationFactory<Program>
    {
        public TestDatabaseFactory() { }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                using var scope = BuildServiceProvider(services).CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<IMongoDbFactory>();

                InitializeDatabase(db);
            });
        }

        private static void InitializeDatabase(IMongoDbFactory db)
        {
            var messagesCollection = db.GetCollection<Message>("messages");
            var chatCollection = db.GetCollection<Chat>("chats");
            messagesCollection.DeleteMany(Builders<Message>.Filter.Empty);
            chatCollection.DeleteMany(Builders<Chat>.Filter.Empty);
        }

        private static ServiceProvider BuildServiceProvider(IServiceCollection services)
        {
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IEventBusSubscriptionsManager)));
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IRabbitMQPersistentConnection)));
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IEventBus)));
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(MongoDBSettings)));
            services.Configure<MongoDBSettings>(options =>
            {
                options.ConnectionURI = "mongodb+srv://fpuser:rJQgWthMythGsp3l@cluster0.gszadiv.mongodb.net/?retryWrites=true&w=majority";
                options.DatabaseName = "freelancer-platform-NotifyChat-test";
            });
            services.AddSingleton<IEventBus>(sp => (new Mock<IEventBus>()).Object);

            return services.BuildServiceProvider();
        }


    }
}
