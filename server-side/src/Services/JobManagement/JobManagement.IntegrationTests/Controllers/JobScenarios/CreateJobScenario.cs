using JobManagement.Application.Commands.JobCommands;
using JobManagement.Application.Queries;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace JobManagement.IntegrationTests.Controllers.JobScenarios
{
    public partial class JobScenarios
    {
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
    }
}
