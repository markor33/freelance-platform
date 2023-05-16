using AutoMapper;
using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
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
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<JobManagementContext>));
            services.Remove(descriptor);
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IEventBusSubscriptionsManager)));
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IRabbitMQPersistentConnection)));
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IEventBus)));
            services.AddSingleton<IEventBus>(sp => (new Mock<IEventBus>()).Object);
            services.AddDbContext<JobManagementContext>(opt =>
            {
                opt.UseNpgsql(CreateConnectionStringForTest());
            });

            return services.BuildServiceProvider();
        }

        private static string CreateConnectionStringForTest()
        {
            return "Server=localhost;Port=43000;Database=job-management.test;Username=postgres;Password=123456;Include Error Detail=true;";
        }

        private static void InitializeDatabase(
            JobManagementContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // FillProfessions(context);
            // FillSkills(context);
            FillJobs(context);

            context.SaveChanges();
        }

        private static void FillJobs(JobManagementContext context)
        {
            context.Database.ExecuteSqlRaw(@"DELETE FROM ""Jobs"" CASCADE;");
            var profession = context.Professions.Find(Guid.Parse("523c9ba1-4e91-4a75-85c3-cf386c078aa9"));
            var job = new Job(
                Guid.Parse("e1372d50-c4af-4f53-9050-457635d49b7c"),
                "Title",
                "Desc",
                ExperienceLevel.JUNIOR,
                new Payment(50, "EUR", PaymentType.FIXED_RATE),
                profession);
            var proposal = new Proposal(
                Guid.Parse("3f006187-d73f-4bdb-bb13-81a4e488d6f8"),
                "Text",
                new Payment(50, "EUR", PaymentType.FIXED_RATE), ProposalStatus.SENT);
            job.AddProposal(proposal);
            context.Jobs.Add(job);
        }

        private static void FillProfessions(JobManagementContext context)
        {
            context.Professions.AddRange(new Profession[]
            {
                new Profession(Guid.Parse("523c9ba1-4e91-4a75-85c3-cf386c078aa9"), "Software engineer", "Software engineer"),
                new Profession(Guid.Parse("71a4d4c7-ed8b-4b6c-ad39-5db767f83c7d"), "Graphical designer", "Graphical designer")
            });
        }

        private static void FillSkills(JobManagementContext context)
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
