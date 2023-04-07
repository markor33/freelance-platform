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
            return new FreelancerController(mediator, null, identityService.Object);
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
            => new(Guid.NewGuid(),
                "Pera",
                "Peric",
                new Contact("Central Europe Standard Time",
                    new Address(
                        "Serbia",
                        "Belgrade",
                        "Knez Mihajlova",
                        "111",
                        "11000"),
                    "0556456561"),
                new ProfileSummary("Title", "Desc"),
                new HourlyRate(10, "USD"),
                Availability.FULL_TIME,
                ExperienceLevel.SENIOR,
                Guid.Parse("523c9ba1-4e91-4a75-85c3-cf386c078aa9"),
                0,
                LanguageProficiencyLevel.NATIVE);
    }
}
