using FreelancerProfile.Application.Commands;
using FreelancerProfile.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace FreelancerProfile.IntegrationTests.Controllers.FreelancerProfileScenarios
{
    public partial class FreelancerProfileScenarios
    {
        
        [Fact]
        public async Task Add_Education_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, Guid.Parse("338391db-978c-4884-8ba0-689da98ed9f1"));
            var addEducationCommand = GetTestAddEducationCommand();

            var result = await controller.AddEducation(addEducationCommand);

            result.Result.ShouldBeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result.Result).Value.ShouldBeOfType(typeof(EducationViewModel));
        }

        private static AddEducationCommand GetTestAddEducationCommand()
            => new(
                "FTN",
                "Dipl. ing.",
                DateTime.Now,
                DateTime.Now.AddMonths(5));

    }
}
