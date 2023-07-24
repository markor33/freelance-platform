using AutoMapper;
using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using FreelancerProfile.Application.IntegrationEvents;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Enums;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using FreelancerProfile.Infrastructure.Persistence;
using FreelancerProfile.Infrastructure.Persistence.ReadModel.Settings;
using IntegrationEventLog.EFCore.Services;
using IntegrationEventLog.EFCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace FreelancerProfile.IntegrationTests.Setup
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
                var db = scopedServices.GetRequiredService<FreelancerProfileContext>();
                var readDb = scopedServices.GetRequiredService<IMongoDbFactory>();

                InitializeDatabase(db, readDb);
            });
        }

        private static ServiceProvider BuildServiceProvider(IServiceCollection services)
        {
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<FreelancerProfileContext>)));
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IEventBusSubscriptionsManager)));
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IRabbitMQPersistentConnection)));
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IEventBus)));
            services.Remove(services.SingleOrDefault(d => d.ImplementationType == typeof(IntegrationEventSenderService)));

            services.Configure<MongoDBSettings>(options =>
            {
                options.ConnectionURI = "mongodb://root:example@localhost:27017/";
                options.DatabaseName = "freelancer-profile-test";
            });

            services.AddSingleton<IEventBus>(sp => (new Mock<IEventBus>()).Object);

            services.AddDbContext<FreelancerProfileContext>(opt =>
            {
                opt.UseNpgsql(CreateConnectionStringForTest());
            });

            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IFreelancerProfileIntegrationEventService)));
            services.AddScoped<IFreelancerProfileIntegrationEventService>(sp => (new Mock<IFreelancerProfileIntegrationEventService>()).Object);

            return services.BuildServiceProvider();
        }

        private static string CreateConnectionStringForTest()
        {
            return "Server=localhost;Port=41000;Database=freelancer-profile.test;Username=postgres;Password=123456;Include Error Detail=true;";
        }

        private static void InitializeDatabase(
            FreelancerProfileContext context,
            IMongoDbFactory db)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.SaveChanges();

            try
            {
                FillLanguages(context);
                FillProfessions(context);
                FillSkills(context);
            }
            catch (Exception ex) { }

            var readFreelancerRepository = db.GetCollection<FreelancerViewModel>();
            readFreelancerRepository.DeleteMany(Builders<FreelancerViewModel>.Filter.Empty);
        }

        private static void FillLanguages(FreelancerProfileContext context)
        {
            context.Database.ExecuteSqlRaw(@"INSERT INTO ""Languages"" (""Id"", ""Name"", ""ShortName"") VALUES (0, 'English', 'en'), (1, 'Serbian', 'sr'), (2, 'German', 'de')");
        }

        private static void FillProfessions(FreelancerProfileContext context)
        {
            context.Database.ExecuteSqlRaw(@"INSERT INTO ""Professions"" (""Id"", ""Name"", ""Description"") VALUES ('523c9ba1-4e91-4a75-85c3-cf386c078aa9', 'Software engineer', 'Software engineer'), ('71a4d4c7-ed8b-4b6c-ad39-5db767f83c7d', 'Graphical designer', 'Graphical designer')");
        }

        private static void FillSkills(FreelancerProfileContext context)
        {
            context.Database.ExecuteSqlRaw(@"INSERT INTO ""Skills"" (""Id"", ""ProfessionId"", ""Name"", ""Description"") VALUES ('02e5ea2e-157d-4801-8733-4e53f268f3d5', '523c9ba1-4e91-4a75-85c3-cf386c078aa9', 'ASP.NET', 'Desc'), ('45f7c95a-5ef9-4791-84cb-51ecb9dcd770', '523c9ba1-4e91-4a75-85c3-cf386c078aa9', 'SignalR', 'Desc'), ('f15e6311-d454-4625-a0ad-397ff111c172', '523c9ba1-4e91-4a75-85c3-cf386c078aa9', 'Java', 'Desc'), ('89cc0246-4653-4bc5-aac0-f865a6a03ecc', '71a4d4c7-ed8b-4b6c-ad39-5db767f83c7d', 'Adobe Photoshop', 'Desc'), ('e704ba7f-9710-469c-96dc-60e1ee4c65f1', '71a4d4c7-ed8b-4b6c-ad39-5db767f83c7d', 'CorelDRAW Graphics Suite', 'Desc')");
        }

    }

    [CollectionDefinition("Database collection", DisableParallelization = true)]
    public class DatabaseCollection
    {
        // This class has no code and its purpose is to be
        // the place to apply [CollectionDefinition] and all the ICollectionFixture<> interfaces.
    }
}
