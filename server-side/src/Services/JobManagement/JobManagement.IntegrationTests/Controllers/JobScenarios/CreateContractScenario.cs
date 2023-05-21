using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace JobManagement.IntegrationTests.Controllers.JobScenarios
{
    public partial class JobScenarios
    {
        [Fact]
        public async Task Make_Contract_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await CreateTestJob(scope);
            var proposal = await CreateTestProposal(scope, job.Id);

            var result = await controller.CreateContract(job.Id, proposal.Id);

            result.ShouldBeOfType(typeof(OkResult));
        }
    }
}
