using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Enums;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using FreelancerProfile.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

                InitializeDatabase(db);
            });
        }

        private static ServiceProvider BuildServiceProvider(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<FreelancerProfileContext>));
            services.Remove(descriptor);
            services.AddDbContext<FreelancerProfileContext>(opt =>
            {
                opt.UseNpgsql(CreateConnectionStringForTest());
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            return services.BuildServiceProvider();
        }

        private static string CreateConnectionStringForTest()
        {
            return "Server=localhost;Port=41000;Database=freelancer-profile.test;Username=postgres;Password=123456;Include Error Detail=true;";
        }

        private static void InitializeDatabase(FreelancerProfileContext context)
        {
            context.Database.EnsureCreated();

            FillFreelancers(context);
            FillLanguages(context);
            FillProfessions(context);

            context.SaveChanges();
        }

        private static void FillFreelancers(FreelancerProfileContext context)
        {
            var address = new Address("Serbia", "Novi Sad", "Tolstojeva", "56", "21000");
            var contact = new Contact("Central Europe Standard Time", address, "064546589");
            var profileSummary = new ProfileSummary("Title", "Desc");
            var hourlyRate = new HourlyRate(25, "EUR");
            var freelancer = new Freelancer(
                Guid.Parse("338391db-978c-4884-8ba0-689da98ed9f1"), 
                "Mika", "Mikic", contact, true, profileSummary,
                hourlyRate, Availability.FULL_TIME, ExperienceLevel.MEDIOR, 
                new Profession(Guid.Parse("523c9ba1-4e91-4a75-85c3-cf386c078aa9"), "Software engineer", "Software engineer"));

            context.Freelancers.Add(freelancer);
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
                new Profession(Guid.Parse("25f54294-8bc0-4ff2-b07d-809ef4a97aae"), "Graphical designer", "Graphical designer")
            });
        }

    }
}
