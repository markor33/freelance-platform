using AutoMapper;
using JobManagement.API.Controllers;
using JobManagement.API.Security;
using JobManagement.Application.Commands.JobCommands;
using JobManagement.Application.Queries;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;
using JobManagement.Infrastructure;
using JobManagement.IntegrationTests.Setup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using Xunit;

namespace JobManagement.IntegrationTests.Controllers
{
    [Collection("JobScenarios")]
    public partial class JobScenarios : BaseIntegrationTest
    {
        public JobScenarios(TestDatabaseFactory factory) : base(factory) { }

        private static JobController SetupController(IServiceScope scope)
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var jobQueries = scope.ServiceProvider.GetRequiredService<IJobQueries>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            var identityService = new Mock<IIdentityService>();
            identityService.Setup(i => i.GetDomainUserId()).Returns(Guid.NewGuid());
            return new JobController(mediator, mapper, jobQueries, identityService.Object);
        }

        [Fact]
        public async Task Create_Job_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var createJobCommand = GetTestCreateJobCommand();

            var result = await controller.Create(createJobCommand);

            result.Result.ShouldBeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result.Result).Value.ShouldBeOfType(typeof(JobViewModel));
        }

        [Fact]
        public async Task Update_Job_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await CreateTestJob(scope);
            var editJobCommand = GetTestUpdateJobCommand(job.Id);

            var result = await controller.Update(editJobCommand);

            result.Result.ShouldBeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result.Result).Value.ShouldBeOfType(typeof(JobViewModel));
        }

        [Fact]
        public async Task Job_Done_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await CreateTestJob(scope);

            var result = await controller.Done(job.Id);

            result.ShouldBeOfType(typeof(OkResult));
        }

        [Fact]
        public async Task Job_Done_HasActiveContracts_ReturnsBadRequest()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await CreateTestJob(scope);
            var contract = await ContractScenarios.CreateTestContract(scope, job);

            var result = await controller.Done(job.Id);

            result.ShouldBeOfType(typeof(BadRequestObjectResult));
        }

        [Fact]
        public async Task Job_Delete_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await CreateTestJob(scope);

            var result = await controller.Delete(job.Id);

            result.ShouldBeOfType(typeof(OkResult));
        }

        [Fact]
        public async Task Job_Delete_HasContracts_ReturnsBadRequest()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await CreateTestJob(scope);
            var contract = await ContractScenarios.CreateTestContract(scope, job);

            var result = await controller.Delete(job.Id);

            result.ShouldBeOfType(typeof(BadRequestObjectResult));
        }

        private static CreateJobCommand GetTestCreateJobCommand()
            => new(
                Guid.Parse("b58921d8-5cec-4339-bbd0-893cdcf30f08"),
                "Title",
                "Desc",
                ExperienceLevel.JUNIOR,
                new Payment(50, "EUR", PaymentType.FIXED_RATE),
                new List<Question>(),
                Guid.Parse("d6861f65-0950-4c7f-b5b1-de644f923fbb"),
                new List<Guid>());

        private static UpdateJobCommand GetTestUpdateJobCommand(Guid jobId)
            => new(jobId,
                "title",
                "desc",
                ExperienceLevel.JUNIOR,
                new Payment(200, "USD", PaymentType.FIXED_RATE),
                new List<Question>(),
                Guid.Parse("d6861f65-0950-4c7f-b5b1-de644f923fbb"),
                new List<Guid>());

        public static async Task<Job> CreateTestJob(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetRequiredService<JobManagementContext>();

            var profession = context.Professions.Find(Guid.Parse("d6861f65-0950-4c7f-b5b1-de644f923fbb"));
            var job = Job.Create(
                Guid.Parse("e1372d50-c4af-4f53-9050-457635d49b7c"),
                "Title",
                "Desc",
                ExperienceLevel.JUNIOR,
                new Payment(50, "EUR", PaymentType.FIXED_RATE),
                profession,
                new List<Question>(),
                new List<Skill>());

            context.Jobs.Add(job);
            await context.SaveChangesAsync();

            return job;
        }

    }
}
