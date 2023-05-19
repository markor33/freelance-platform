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
        public async Task Freelancer_Accept_Proposal_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await CreateTestJob(scope);
            var proposal = await CreateTestProposal(scope, job.Id);
            var freelancerAcceptProposalCommand = GetTestFreelancerAcceptProposalCommand(job.Id, proposal.Id);

            var result = await controller.FreelancerAcceptProposal(freelancerAcceptProposalCommand);

            result.ShouldBeOfType(typeof(OkResult));
        }

        private static FreelancerAcceptProposalCommand GetTestFreelancerAcceptProposalCommand(Guid jobId, Guid proposalId)
            => new(jobId, proposalId);
    }
}
