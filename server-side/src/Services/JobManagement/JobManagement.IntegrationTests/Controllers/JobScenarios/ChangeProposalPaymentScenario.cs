using JobManagement.Application.Commands.ProposalCommands;
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
        public async Task Change_Proposal_Payment_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await CreateTestJob(scope);
            var proposal = await CreateTestProposal(scope, job.Id);
            var editProposalPaymentCommand = GetTestEditProposalPaymentCommand(job.Id, proposal.Id);

            var result = await controller.EditProposalPayment(editProposalPaymentCommand);

            result.ShouldBeOfType(typeof(OkResult));
        }

        private static EditProposalPaymentCommand GetTestEditProposalPaymentCommand(Guid jobId, Guid proposalId)
            => new(jobId, proposalId, new Payment(300, "EUR", PaymentType.FIXED_RATE));

    }
}
