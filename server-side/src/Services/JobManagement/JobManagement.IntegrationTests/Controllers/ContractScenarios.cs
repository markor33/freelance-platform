using JobManagement.API.Controllers;
using JobManagement.API.Security;
using JobManagement.Application.Queries;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Infrastructure;
using JobManagement.IntegrationTests.Setup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace JobManagement.IntegrationTests.Controllers
{
    public class ContractScenarios : BaseIntegrationTest
    {
        public ContractScenarios(TestDatabaseFactory factory) : base(factory) { }

        private static ContractController SetupController(IServiceScope scope)
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var identityService = scope.ServiceProvider.GetRequiredService<IIdentityService>();
            var contractQueries = scope.ServiceProvider.GetRequiredService<IContractQueries>();
            return new ContractController(mediator, identityService, contractQueries);
        }

        [Fact]
        public async Task Make_Contract_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await JobScenarios.CreateTestJob(scope);
            var proposal = await ProposalScenarios.CreateTestProposal(scope, job, ProposalStatus.CLIENT_APPROVED);

            var result = await controller.Create(job.Id, proposal.Id);

            result.ShouldBeOfType(typeof(OkResult));
        }

        [Fact]
        public async Task Finish_Contract_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await JobScenarios.CreateTestJob(scope);
            var contract = await CreateTestContract(scope, job);

            var result = await controller.Finish(job.Id, contract.Id);

            result.ShouldBeOfType(typeof(OkResult));
        }

        [Fact]
        public async Task Terminate_Contract_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var job = await JobScenarios.CreateTestJob(scope);
            var contract = await CreateTestContract(scope, job);

            var result = await controller.Terminate(job.Id, contract.Id);

            result.ShouldBeOfType(typeof(OkResult));
        }

        public static async Task<Contract> CreateTestContract(IServiceScope scope, Job job)
        {
            var context = scope.ServiceProvider.GetRequiredService<JobManagementContext>();

            var proposal = await ProposalScenarios.CreateTestProposal(scope, job, ProposalStatus.CLIENT_APPROVED);
            var contract = job.MakeContract(proposal.Id);

            await context.SaveChangesAsync();

            return contract.Value;
        }

    }
}
