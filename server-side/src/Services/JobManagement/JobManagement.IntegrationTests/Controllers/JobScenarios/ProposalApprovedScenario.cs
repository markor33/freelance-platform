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
        public async Task Approve_Proposal_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await CreateTestJob(scope);
            var proposal = await CreateTestProposal(scope, job.Id);
            var approveProposalCommand = GetTestApproveProposalCommand(job.Id, proposal.Id);

            var result = await controller.ApproveProposal(job.Id, proposal.Id);

            result.ShouldBeOfType(typeof(OkResult));
        }

        private static ApproveProposalCommand GetTestApproveProposalCommand(Guid jobId, Guid proposalId)
            => new(jobId, proposalId);
    }
}
