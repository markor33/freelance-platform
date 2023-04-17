using FreelancerProfile.API.Controllers;
using FreelancerProfile.API.Security;
using FreelancerProfile.Application.Commands;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Enums;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using FreelancerProfile.IntegrationTests.Setup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using Xunit;

namespace FreelancerProfile.IntegrationTests
{
    [Collection("FreelancerProfileScenarios")]
    public class FreelancerProfileScenarios : BaseIntegrationTest
    {

        public FreelancerProfileScenarios(TestDatabaseFactory factory) : base(factory) { }

        private static FreelancerController SetupController(IServiceScope scope, Guid userId)
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var identityService = new Mock<IIdentityService>();
            identityService.Setup(i => i.GetUserId()).Returns(userId);
            return new FreelancerController(mediator, null, identityService.Object, null);
        }

        [Fact]
        public async Task Create_Freelancer_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, Guid.NewGuid());
            var createFreelancerCommand = GetTestCreateFreelancerCommand();

            var result = await controller.Create(createFreelancerCommand);

            result.Result.ShouldBeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result.Result).Value.ShouldBeOfType(typeof(Freelancer));
        }

        [Fact]
        public async Task Create_FreelancerAlreadyCreated_ReturnsBadRequest()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, Guid.Parse("338391db-978c-4884-8ba0-689da98ed9f1"));
            var createFreelancerCommand = GetTestCreateFreelancerCommand();

            var result = await controller.Create(createFreelancerCommand);

            result.Result.ShouldBeOfType(typeof(BadRequestResult));
        }


        private static CreateFreelancerCommand GetTestCreateFreelancerCommand()
            => new();
    }
}
