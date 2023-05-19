using JobManagement.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace JobManagement.IntegrationTests.Controllers.JobScenarios
{
    public partial class JobScenarios
    {
        [Fact]
        public async Task Client_Accept_Proposal_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await CreateTestJob(scope);
            var proposal = await CreateTestProposal(scope, job.Id);
            var clientAcceptProposalCommand = GetTestClientAcceptProposalCommand(job.Id, proposal.Id);

            var result = await controller.ClientAcceptProposal(clientAcceptProposalCommand);

            result.ShouldBeOfType(typeof(OkResult));
        }

        private static ClientAcceptProposalCommand GetTestClientAcceptProposalCommand(Guid jobId, Guid proposalId)
            => new(jobId, proposalId);
    }
}
