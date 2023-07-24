using AutoMapper;
using FreelancerProfile.API.Controllers;
using FreelancerProfile.API.Security;
using FreelancerProfile.Application.Commands;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using FreelancerProfile.Infrastructure.Persistence;
using FreelancerProfile.IntegrationTests.Setup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using Xunit;

namespace FreelancerProfile.IntegrationTests.Controllers
{
    [Collection("Database collection")]
    public class EmploymentScenarios : BaseIntegrationTest
    {
        public EmploymentScenarios(TestDatabaseFactory factory) : base(factory) { }

        private static EmploymentController SetupController(IServiceScope scope, Guid freelancerId)
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            var identityService = new Mock<IIdentityService>();
            identityService.Setup(i => i.GetDomainUserId()).Returns(freelancerId);
            return new EmploymentController(mediator, identityService.Object, mapper);
        }

        [Fact]
        public async Task Add_Employment_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var freelancer = await FreelancerProfileScenarios.CreateTestFreelancer(scope);
            var controller = SetupController(scope, freelancer.Id);
            var addEmploymentCommand = GetTestAddEmploymentCommand();

            var result = await controller.AddEmployment(addEmploymentCommand);

            result.Result.ShouldBeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result.Result).Value.ShouldBeOfType(typeof(EmploymentViewModel));
        }

        [Fact]
        public async Task Update_Employment_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var freelancer = await FreelancerProfileScenarios.CreateTestFreelancer(scope);
            var employment = await CreateTestEmployment(scope, freelancer);
            var controller = SetupController(scope, freelancer.Id);
            var updateEmploymentCommand = GetTestUpdateEmploymentCommand(employment.Id);

            var result = await controller.Update(updateEmploymentCommand);

            result.ShouldBeOfType(typeof(OkResult));
        }

        [Fact]
        public async Task Delete_Employment_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var freelancer = await FreelancerProfileScenarios.CreateTestFreelancer(scope);
            var employment = await CreateTestEmployment(scope, freelancer);
            var controller = SetupController(scope, freelancer.Id);

            var result = await controller.Delete(freelancer.Id, employment.Id);

            result.ShouldBeOfType(typeof(OkResult));
        }

        private static AddEmploymentCommand GetTestAddEmploymentCommand()
            => new(
                "Microsoft",
                "Software Engineer",
                DateTime.Now,
                DateTime.Now.AddMonths(5),
                "Desc");

        private static UpdateEmploymentCommand GetTestUpdateEmploymentCommand(Guid employmentId)
            => new(
                employmentId,
                "company",
                "title",
                DateTime.Now,
                DateTime.Now.AddMonths(1),
                "desc");

        private static async Task<Employment> CreateTestEmployment(IServiceScope scope, Freelancer freelancer)
        {
            var context = scope.ServiceProvider.GetRequiredService<FreelancerProfileContext>();

            var employment = new Employment("company", "title", new DateRange(DateTime.Now, DateTime.Now.AddMonths(1)), "desc");
            freelancer.AddEmployment(employment);

            await context.SaveChangesAsync();

            return employment;
        }
    }
}
