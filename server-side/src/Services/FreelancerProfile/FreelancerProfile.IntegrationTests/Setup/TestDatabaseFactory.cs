using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FreelancerProfile.IntegrationTests.Setup
{
    public class TestDatabaseFactory : WebApplicationFactory<Program>
    {
        public TestDatabaseFactory() 
        {
            var a = 1;
        }

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
            return "Server=host.docker.internal;Port=41000;Database=freelancer-profile.test;Username=postgres;Password=123456";
        }

        private static void InitializeDatabase(FreelancerProfileContext context)
        {
            context.Database.EnsureCreated();

            FillFreelancers(context);

            context.SaveChanges();
        }

        private static void FillFreelancers(FreelancerProfileContext context)
        {
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Freelancers\";");

            var address = new Address("Serbia", "Novi Sad", "Tolstojeva", "56", "21000");
            var contact = new Contact("Central Europe Standard Time", address, "064546589");
            var freelancer = new Freelancer(Guid.Parse("338391db-978c-4884-8ba0-689da98ed9f1"), "Filip", "Vujovic", contact);

            context.Freelancers.Add(freelancer);
        }

    }
}
