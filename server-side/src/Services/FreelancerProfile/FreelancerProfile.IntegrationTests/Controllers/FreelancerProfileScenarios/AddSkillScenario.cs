using FreelancerProfile.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Net;
using Xunit;

namespace FreelancerProfile.IntegrationTests.Controllers.FreelancerProfileScenarios
{
    public partial class FreelancerProfileScenarios
    {
        [Fact]
        public async Task Add_Skill_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, Guid.Parse("338391db-978c-4884-8ba0-689da98ed9f1"));
            var addSkillCommand = GetTestAddSkillCommand(GetSkillsType1());

            var result = await controller.AddSkill(addSkillCommand);

            result.ShouldBeOfType(typeof(OkResult));
        }

        [Fact]
        public async Task Add_SkillFromOtherProfessionType_ReturnsBadRequest()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, Guid.Parse("338391db-978c-4884-8ba0-689da98ed9f1"));
            var addSkillCommand = GetTestAddSkillCommand(GetSkillsType2());

            var result = await controller.AddSkill(addSkillCommand);

            result.ShouldBeOfType(typeof(BadRequestObjectResult));
        }

        [Fact]
        public async Task Add_SkillAlreadyAdded_ReturnsBadRequest()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope, Guid.Parse("338391db-978c-4884-8ba0-689da98ed9f1"));
            var addSkillCommand = GetTestAddSkillCommand(new List<Guid>() { Guid.Parse("f15e6311-d454-4625-a0ad-397ff111c172") });

            var result = await controller.AddSkill(addSkillCommand);

            result.ShouldBeOfType(typeof(BadRequestObjectResult));
        }

        private static AddSkillCommand GetTestAddSkillCommand(List<Guid> skills) => new(skills);

        private static List<Guid> GetSkillsType1()
            => new(
                new List<Guid>()
                {
                    Guid.Parse("02e5ea2e-157d-4801-8733-4e53f268f3d5"),
                    Guid.Parse("45f7c95a-5ef9-4791-84cb-51ecb9dcd770")
                });

        private static List<Guid> GetSkillsType2()
            => new(
                new List<Guid>()
                {
                    Guid.Parse("89cc0246-4653-4bc5-aac0-f865a6a03ecc"),
                    Guid.Parse("e704ba7f-9710-469c-96dc-60e1ee4c65f1")
                });
    }
}
