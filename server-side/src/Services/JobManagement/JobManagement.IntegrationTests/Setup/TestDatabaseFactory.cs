using AutoMapper;
using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using IntegrationEventLog.EFCore.Services;
using JobManagement.Application.IntegrationEvents;
using JobManagement.Application.Queries;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;
using JobManagement.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace JobManagement.IntegrationTests.Setup
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
                var db = scopedServices.GetRequiredService<JobManagementContext>();
                var mapper = scopedServices.GetRequiredService<IMapper>();

                InitializeDatabase(db);
            });
        }

        private static ServiceProvider BuildServiceProvider(IServiceCollection services)
        {
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<JobManagementContext>)));
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IEventBusSubscriptionsManager)));
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IRabbitMQPersistentConnection)));
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IEventBus)));
            services.Remove(services.SingleOrDefault(d => d.ImplementationType == typeof(IntegrationEventSenderService)));

            services.AddSingleton<IEventBus>(sp => (new Mock<IEventBus>()).Object);

            services.AddDbContext<JobManagementContext>(opt =>
            {
                opt.UseNpgsql(CreateConnectionStringForTest());
            });

            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IJobIntegrationEventService)));
            services.AddScoped<IJobIntegrationEventService>(sp => (new Mock<IJobIntegrationEventService>()).Object);

            return services.BuildServiceProvider();
        }

        private static string CreateConnectionStringForTest()
        {
            return "Server=localhost;Port=41000;Database=job-management.test;Username=postgres;Password=123456;Include Error Detail=true;";
        }

        private static void InitializeDatabase(
            JobManagementContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.SaveChanges();

            try
            {
                FillProfessions(context);
                FillSkills(context);
            }
            catch (Exception ex) { }

        }

        private static void FillProfessions(JobManagementContext context)
        {
            context.Database.ExecuteSqlRaw(@"INSERT INTO ""Professions"" (""Id"", ""Name"", ""Description"") VALUES ('523c9ba1-4e91-4a75-85c3-cf386c078aa9', 'Software engineer', 'Software engineer'), ('71a4d4c7-ed8b-4b6c-ad39-5db767f83c7d', 'Graphical designer', 'Graphical designer')");
        }

        private static void FillSkills(JobManagementContext context)
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
