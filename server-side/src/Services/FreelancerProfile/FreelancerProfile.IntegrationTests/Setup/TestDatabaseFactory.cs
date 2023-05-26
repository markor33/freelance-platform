using AutoMapper;
using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Enums;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using FreelancerProfile.Infrastructure;
using FreelancerProfile.Infrastructure.ReadModel.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Moq;

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
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<FreelancerProfileContext>));
            services.Remove(descriptor);
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IEventBusSubscriptionsManager)));
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IRabbitMQPersistentConnection)));
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IEventBus)));
            services.Configure<MongoDBSettings>(options =>
            {
                options.DatabaseName= "freelancer-profile-test";
            });
            services.AddSingleton<IEventBus>(sp => (new Mock<IEventBus>()).Object);
            services.AddDbContext<FreelancerProfileContext>(opt =>
            {
                opt.UseNpgsql(CreateConnectionStringForTest());
            });

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
            context.Database.EnsureCreated();
            context.Database.ExecuteSqlRaw(@"DELETE FROM ""Freelancers"" CASCADE;");
            context.SaveChanges();

            var readFreelancerRepository = db.GetCollection<FreelancerViewModel>();
            readFreelancerRepository.DeleteMany(Builders<FreelancerViewModel>.Filter.Empty);
        }

        private static void FillLanguages(FreelancerProfileContext context)
        {
            context.Languages.AddRange(new Language[] 
            { 
                new Language(0, "English", "en"),
                new Language(1, "Serbian", "sr"),
                new Language(2, "German", "de")
            });
        }

        private static void FillProfessions(FreelancerProfileContext context)
        {
            context.Professions.AddRange(new Profession[] 
            {
                new Profession(Guid.Parse("523c9ba1-4e91-4a75-85c3-cf386c078aa9"), "Software engineer", "Software engineer"),
                new Profession(Guid.Parse("71a4d4c7-ed8b-4b6c-ad39-5db767f83c7d"), "Graphical designer", "Graphical designer")
            });
        }

        private static void FillSkills(FreelancerProfileContext context)
        {
            context.Skills.AddRange(new Skill[]
            {
                new Skill(Guid.Parse("02e5ea2e-157d-4801-8733-4e53f268f3d5"), Guid.Parse("523c9ba1-4e91-4a75-85c3-cf386c078aa9"), "ASP.NET", "Desc"),
                new Skill(Guid.Parse("45f7c95a-5ef9-4791-84cb-51ecb9dcd770"), Guid.Parse("523c9ba1-4e91-4a75-85c3-cf386c078aa9"), "SignalR", "Desc"),
                new Skill(Guid.Parse("f15e6311-d454-4625-a0ad-397ff111c172"), Guid.Parse("523c9ba1-4e91-4a75-85c3-cf386c078aa9"), "Java", "Desc"),
                new Skill(Guid.Parse("89cc0246-4653-4bc5-aac0-f865a6a03ecc"), Guid.Parse("71a4d4c7-ed8b-4b6c-ad39-5db767f83c7d"), "Adobe Photoshop", "Desc"),
                new Skill(Guid.Parse("e704ba7f-9710-469c-96dc-60e1ee4c65f1"), Guid.Parse("71a4d4c7-ed8b-4b6c-ad39-5db767f83c7d"), "CorelDRAW Graphics Suite", "Desc")
            });
        }

    }
}
