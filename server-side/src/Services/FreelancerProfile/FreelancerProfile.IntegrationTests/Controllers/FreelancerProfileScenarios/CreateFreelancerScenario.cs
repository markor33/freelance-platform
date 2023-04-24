using FreelancerProfile.Application.Commands;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Enums;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace FreelancerProfile.IntegrationTests.Controllers.FreelancerProfileScenarios
{
    public partial class FreelancerProfileScenarios
    {
        
        [Fact]
        public async Task Create_Freelancer_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, Guid.NewGuid());
            var createFreelancerCommand = GetTestCreateFreelancerCommand();

            var result = await controller.Create(createFreelancerCommand);

            result.Result.ShouldBeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result.Result).Value.ShouldBeOfType(typeof(FreelancerViewModel));
        }

        [Fact]
        public async Task Create_FreelancerAlreadyCreated_ReturnsBadRequest()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, Guid.Parse("338391db-978c-4884-8ba0-689da98ed9f1"));
            var createFreelancerCommand = GetTestCreateFreelancerCommand();

            var result = await controller.Create(createFreelancerCommand);

            result.Result.ShouldBeOfType(typeof(BadRequestObjectResult));
        }

        private static CreateFreelancerCommand GetTestCreateFreelancerCommand()
            => new(
                Guid.Parse("3984e251-cdd0-4674-82b9-0583de3d0a0b"),
                "Pera",
                "Peric",
                new Contact("Central Europe Standard Time",
                    new Address("Serbia", "Belgrade", "Gagarinova", "1", "11000"),
                    "06548989298"),
                true,
                new ProfileSummary("Title", "Desc"),
                new HourlyRate(50, "USD"),
                Availability.FULL_TIME,
                ExperienceLevel.MEDIOR,
                Guid.Parse("523c9ba1-4e91-4a75-85c3-cf386c078aa9"),
                1,
                LanguageProficiencyLevel.NATIVE);
    }
}
