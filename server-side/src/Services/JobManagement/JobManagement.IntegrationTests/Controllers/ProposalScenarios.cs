using AutoMapper;
using JobManagement.API.Controllers;
using JobManagement.API.Security;
using JobManagement.Application.Commands.ProposalCommands;
using JobManagement.Application.Queries;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;
using JobManagement.Infrastructure;
using JobManagement.IntegrationTests.Setup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace JobManagement.IntegrationTests.Controllers
{
    public class ProposalScenarios : BaseIntegrationTest
    {
        public ProposalScenarios(TestDatabaseFactory factory) : base(factory) { }

        private static ProposalController SetupController(IServiceScope scope)
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var proposalQueries = scope.ServiceProvider.GetRequiredService<IProposalQueries>();
            var answerQueries = scope.ServiceProvider.GetRequiredService<IAnswerQueries>();
            var identityService = scope.ServiceProvider.GetRequiredService<IIdentityService>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            return new ProposalController(mediator, identityService, proposalQueries, answerQueries, mapper);
        }

        [Fact]
        public async Task Create_Proposal_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await JobScenarios.CreateTestJob(scope);
            var createProposalCommand = GetTestCreateProposalCommand(job.Id);

            var result = await controller.Create(createProposalCommand);

            result.Result.ShouldBeOfType(typeof(AcceptedResult));
            ((AcceptedResult)result.Result).Value.ShouldBeOfType(typeof(ProposalViewModel));
        }

        [Fact]
        public async Task Create_ProposalAlreadyCreated_ReturnsBadRequest()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await JobScenarios.CreateTestJob(scope);
            var proposal = await CreateTestProposal(scope, job);
            var createProposalCommand = GetTestCreateProposalCommand(job.Id);

            var result = await controller.Create(createProposalCommand);

            result.Result.ShouldBeOfType(typeof(BadRequestObjectResult));
        }

        [Fact]
        public async Task Approve_Proposal_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await JobScenarios.CreateTestJob(scope);
            var proposal = await CreateTestProposal(scope, job);
            var approveProposalCommand = GetTestApproveProposalCommand(job.Id, proposal.Id);

            var result = await controller.Approve(job.Id, proposal.Id);

            result.ShouldBeOfType(typeof(OkResult));
        }

        [Fact]
        public async Task Update_Proposal_Payment_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await JobScenarios.CreateTestJob(scope);
            var proposal = await CreateTestProposal(scope, job);
            var editProposalPaymentCommand = GetTestUpdateProposalPaymentCommand(job.Id, proposal.Id);

            var result = await controller.UpdatePayment(editProposalPaymentCommand);

            result.ShouldBeOfType(typeof(OkResult));
        }

        private static UpdateProposalPaymentCommand GetTestUpdateProposalPaymentCommand(Guid jobId, Guid proposalId)
            => new(jobId, proposalId, new Payment(300, "EUR", PaymentType.FIXED_RATE));

        private static ApproveProposalCommand GetTestApproveProposalCommand(Guid jobId, Guid proposalId)
            => new(jobId, proposalId);

        private static CreateProposalCommand GetTestCreateProposalCommand(Guid jobId)
            => new(
                Guid.Parse("eacfeae5-f0fb-4f91-a6e6-514de27bab57"),
                jobId,
                "Text",
                new Payment(50, "EUR", PaymentType.FIXED_RATE),
                new List<Answer>());

        public static async Task<Proposal> CreateTestProposal(IServiceScope scope, Job job, ProposalStatus proposalStatus = ProposalStatus.SENT)
        {
            var context = scope.ServiceProvider.GetRequiredService<JobManagementContext>();

            var proposal = Proposal.Create(
                Guid.Parse("eacfeae5-f0fb-4f91-a6e6-514de27bab57"),
                "Text",
                new Payment(50, "EUR", PaymentType.FIXED_RATE), new List<Answer>(), proposalStatus);

            job.AddProposal(proposal);
            await context.SaveChangesAsync();

            return proposal;
        }
    }
}
