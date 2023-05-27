using AutoMapper;
using FreelancerProfile.API.Controllers;
using FreelancerProfile.API.Security;
using FreelancerProfile.Application.Commands;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using FreelancerProfile.Infrastructure;
using FreelancerProfile.IntegrationTests.Setup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using Xunit;

namespace FreelancerProfile.IntegrationTests.Controllers
{
    public class EducationScenarios : BaseIntegrationTest
    {

        public EducationScenarios(TestDatabaseFactory factory) : base(factory) { }

        private static EducationController SetupController(IServiceScope scope, Guid freelancerId)
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            var identityService = new Mock<IIdentityService>();
            identityService.Setup(i => i.GetDomainUserId()).Returns(freelancerId);
            return new EducationController(mediator, identityService.Object, mapper);
        }

        [Fact]
        public async Task Add_Education_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var freelancer = await FreelancerProfileScenarios.CreateTestFreelancer(scope);
            var controller = SetupController(scope, freelancer.Id);
            var addEducationCommand = GetTestAddEducationCommand();

            var result = await controller.AddEducation(addEducationCommand);

            result.Result.ShouldBeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result.Result).Value.ShouldBeOfType(typeof(EducationViewModel));
        }

        [Fact]
        public async Task Update_Education_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var freelancer = await FreelancerProfileScenarios.CreateTestFreelancer(scope);
            var education = await CreateTestEducation(scope, freelancer);
            var controller = SetupController(scope, freelancer.Id);
            var updateEducationCommand = GetTestUpdateEducationCommand(education.Id);

            var result = await controller.Update(updateEducationCommand);

            result.Result.ShouldBeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result.Result).Value.ShouldBeOfType(typeof(EducationViewModel));
        }

        [Fact]
        public async Task Delete_Education_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var freelancer = await FreelancerProfileScenarios.CreateTestFreelancer(scope);
            var education = await CreateTestEducation(scope, freelancer);
            var controller = SetupController(scope, freelancer.Id);

            var result = await controller.Delete(freelancer.Id, education.Id);

            result.ShouldBeOfType(typeof(OkResult));
        }

        private static AddEducationCommand GetTestAddEducationCommand()
            => new(
                "FTN",
                "Dipl. ing.",
                DateTime.Now,
                DateTime.Now.AddMonths(5));

        private static UpdateEducationCommand GetTestUpdateEducationCommand(Guid educationId)
            => new(
                educationId,
                "schoolName",
                "degree",
                DateTime.Now,
                DateTime.Now.AddMonths(1)
                );

        private static async Task<Education> CreateTestEducation(IServiceScope scope, Freelancer freelancer)
        {
            var context = scope.ServiceProvider.GetRequiredService<FreelancerProfileContext>();

            var education = new Education("schoolName", "degree", new DateRange(DateTime.Now, DateTime.Now.AddMonths(1)));
            freelancer.AddEducation(education);

            await context.SaveChangesAsync();

            return education;
        }

    }
}
