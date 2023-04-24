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
        public async Task Add_Certification_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, Guid.Parse("338391db-978c-4884-8ba0-689da98ed9f1"));
            var addCertificationCommand = GetTestAddCertificationCommand();

            var result = await controller.AddCertification(addCertificationCommand);

            result.Result.ShouldBeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result.Result).Value.ShouldBeOfType(typeof(CertificationViewModel));
        }

        private static AddCertificationCommand GetTestAddCertificationCommand()
            => new(
                "Azure",
                "Microsoft",
                "Desc",
                DateTime.Now,
                DateTime.Now.AddMonths(5));
    }
}
