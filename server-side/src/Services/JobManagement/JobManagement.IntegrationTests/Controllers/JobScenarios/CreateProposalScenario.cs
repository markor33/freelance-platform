using JobManagement.Application.Commands.ProposalCommands;
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
        public async Task Create_Proposal_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await CreateTestJob(scope);
            var createProposalCommand = GetTestCreateProposalCommand(job.Id);

            var result = await controller.CreateProposal(createProposalCommand);

            result.Result.ShouldBeOfType(typeof(AcceptedResult));
            ((AcceptedResult)result.Result).Value.ShouldBeOfType(typeof(ProposalViewModel));
        }

        [Fact]
        public async Task Create_ProposalAlreadyCreated_ReturnsBadRequest()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await CreateTestJob(scope);
            var proposal = await CreateTestProposal(scope, job.Id);
            var createProposalCommand = GetTestCreateProposalCommand(job.Id);

            var result = await controller.CreateProposal(createProposalCommand);

            result.Result.ShouldBeOfType(typeof(BadRequestObjectResult));
        }

        private static CreateProposalCommand GetTestCreateProposalCommand(Guid jobId)
            => new(
                Guid.Parse("eacfeae5-f0fb-4f91-a6e6-514de27bab57"),
                jobId,
                "Text",
                new Payment(50, "EUR", PaymentType.FIXED_RATE),
                new List<Answer>());
    }
}
