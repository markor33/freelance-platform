using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace JobManagement.IntegrationTests.Controllers.JobScenarios
{
    public partial class JobScenarios
    {
        [Fact]
        public async Task Job_Done_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await CreateTestJob(scope);

            var result = await controller.Done(job.Id);

            result.ShouldBeOfType(typeof(OkResult));
        }
    }
}
