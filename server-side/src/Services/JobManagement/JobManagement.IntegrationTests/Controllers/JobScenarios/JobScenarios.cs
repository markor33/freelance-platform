using AutoMapper;
using JobManagement.API.Controllers;
using JobManagement.Application.Queries;
using JobManagement.Application.Services;
using JobManagement.Domain.AggregatesModel.JobAggregate;
using JobManagement.Domain.AggregatesModel.JobAggregate.Entities;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;
using JobManagement.IntegrationTests.Setup;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace JobManagement.IntegrationTests.Controllers.JobScenarios
{
    [Collection("JobScenarios")]
    public partial class JobScenarios : BaseIntegrationTest
    {
        public JobScenarios(TestDatabaseFactory factory) : base(factory) { }

        private static JobController SetupController(IServiceScope scope)
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var jobQueries = scope.ServiceProvider.GetRequiredService<IJobQueries>();
            var proposalQueries = scope.ServiceProvider.GetRequiredService<IProposalQueries>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            return new JobController(mediator, mapper, jobQueries, proposalQueries);
        }

        private static async Task<Job> CreateTestJob(IServiceScope scope)
        {
            var jobRepository = scope.ServiceProvider.GetRequiredService<IJobRepository>();
            var professionService = scope.ServiceProvider.GetRequiredService<IProfessionService>();

            var profession = await professionService.GetByIdAsync(Guid.Parse("523c9ba1-4e91-4a75-85c3-cf386c078aa9"));
            var job = new Job(
                Guid.Parse("e1372d50-c4af-4f53-9050-457635d49b7c"),
                "Title",
                "Desc",
                ExperienceLevel.JUNIOR,
                new Payment(50, "EUR", PaymentType.FIXED_RATE),
                profession);

            job = await jobRepository.CreateAsync(job);
            await jobRepository.UnitOfWork.SaveEntitiesAsync();
            return job;
        }

        private static async Task<Guid> CreateTestProposal(IServiceScope scope)
        {
            var jobRepository = scope.ServiceProvider.GetRequiredService<IJobRepository>();
            var job = await CreateTestJob(scope);

            var proposal = new Proposal(
                Guid.Parse("eacfeae5-f0fb-4f91-a6e6-514de27bab57"),
                "Text",
                new Payment(50, "EUR", PaymentType.FIXED_RATE),
                ProposalStatus.SENT);
            job.AddProposal(proposal);

            await jobRepository.UnitOfWork.SaveEntitiesAsync();
            return job.Id;
        }

    }
}
