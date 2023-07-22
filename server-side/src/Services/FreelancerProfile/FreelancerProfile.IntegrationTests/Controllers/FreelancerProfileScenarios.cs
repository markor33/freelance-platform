using AutoMapper;
using FreelancerProfile.API.Controllers;
using FreelancerProfile.API.Security;
using FreelancerProfile.Application.Commands;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Enums;
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
    [Collection("FreelancerProfileScenarios")]
    public partial class FreelancerProfileScenarios : BaseIntegrationTest
    {

        public FreelancerProfileScenarios(TestDatabaseFactory factory) : base(factory) { }

        private static FreelancerController SetupController(IServiceScope scope, Guid freelancerId)
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var freelancerQueries = scope.ServiceProvider.GetRequiredService<IFreelancerQueries>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            var identityService = new Mock<IIdentityService>();
            identityService.Setup(i => i.GetDomainUserId()).Returns(freelancerId);
            return new FreelancerController(mediator, freelancerQueries, identityService.Object, mapper);
        }

        [Fact]
        public async Task Setup_Profile_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var freelancer = await CreateTestFreelancer(scope);
            var controller = SetupController(scope, freelancer.Id);
            var createFreelancerCommand = GetTestProfileSetupCommand();

            var result = await controller.SetupProfile(createFreelancerCommand);

            result.Result.ShouldBeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result.Result).Value.ShouldBeOfType(typeof(FreelancerViewModel));
        }

        private static ProfileSetupCommand GetTestProfileSetupCommand()
            => new(
                true,
                new ProfileSummary("Title", "Desc"),
                new HourlyRate(50, "USD"),
                Availability.FULL_TIME,
                ExperienceLevel.MEDIOR,
                Guid.Parse("d6861f65-0950-4c7f-b5b1-de644f923fbb"),
                1,
                LanguageProficiencyLevel.NATIVE);

        public static async Task<Freelancer> CreateTestFreelancer(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetRequiredService<FreelancerProfileContext>();
            var address = new Address("Serbia", "Novi Sad", "Tolstojeva", "56", "21000");
            var contact = new Contact("Central Europe Standard Time", address, "064546589");
            var profileSummary = new ProfileSummary("Title", "Desc");
            var hourlyRate = new HourlyRate(25, "EUR");
            var profession = context.Professions.Find(Guid.Parse("523c9ba1-4e91-4a75-85c3-cf386c078aa9"));
            var freelancer = Freelancer.Create(Guid.NewGuid(), "Pera" , "Peric", contact);
            context.Freelancers.Add(freelancer);
            await context.SaveChangesAsync();

            return freelancer;
        }

    }
}
