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
            var answerQueries = scope.ServiceProvider.GetRequiredService<IAnswerQueries>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            return new JobController(mediator, mapper, jobQueries, proposalQueries, answerQueries);
        }

        private static async Task<Job> CreateTestJob(IServiceScope scope)
        {
            var jobRepository = scope.ServiceProvider.GetRequiredService<IJobRepository>();
            var professionService = scope.ServiceProvider.GetRequiredService<IProfessionService>();

            var profession = await professionService.GetByIdAsync(Guid.Parse("d6861f65-0950-4c7f-b5b1-de644f923fbb"));
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

        private static async Task<Proposal> CreateTestProposal(IServiceScope scope, Guid jobId)
        {
            var jobRepository = scope.ServiceProvider.GetRequiredService<IJobRepository>();
            var job = await jobRepository.GetByIdAsync(jobId);

            var proposal = new Proposal(
                Guid.Parse("eacfeae5-f0fb-4f91-a6e6-514de27bab57"),
                "Text",
                new Payment(50, "EUR", PaymentType.FIXED_RATE),
                ProposalStatus.SENT);
            job.AddProposal(proposal);

            await jobRepository.UnitOfWork.SaveEntitiesAsync();
            return proposal;
        }

    }
}
