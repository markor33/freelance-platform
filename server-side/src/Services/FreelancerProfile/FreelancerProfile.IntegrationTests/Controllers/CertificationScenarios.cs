using AutoMapper;
using FreelancerProfile.API.Controllers;
using FreelancerProfile.API.Security;
using FreelancerProfile.Application.Commands;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.Application.Validations;
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
    public class CertificationScenarios : BaseIntegrationTest
    {

        public CertificationScenarios(TestDatabaseFactory factory) : base(factory) { }

        private static CertificationController SetupController(IServiceScope scope, Guid freelancerId)
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            var identityService = new Mock<IIdentityService>();
            identityService.Setup(i => i.GetDomainUserId()).Returns(freelancerId);
            return new CertificationController(mediator, identityService.Object, mapper);
        }

        [Fact]
        public async Task Add_Certification_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var freelancer = await FreelancerProfileScenarios.CreateTestFreelancer(scope);
            var controller = SetupController(scope, freelancer.Id);
            var addCertificationCommand = GetTestAddCertificationCommand();

            var result = await controller.AddCertification(addCertificationCommand);

            result.Result.ShouldBeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result.Result).Value.ShouldBeOfType(typeof(CertificationViewModel));
        }

        [Fact]
        public async Task Delete_Certification_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var freelancer = await FreelancerProfileScenarios.CreateTestFreelancer(scope);
            var certification = await CreateTestCertification(scope, freelancer);
            var controller = SetupController(scope, freelancer.Id);

            var result = await controller.Delete(freelancer.Id, certification.Id);

            result.ShouldBeOfType(typeof(OkResult));
        }

        [Fact]
        public async Task Update_Certification_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var freelancer = await FreelancerProfileScenarios.CreateTestFreelancer(scope);
            var certification = await CreateTestCertification(scope, freelancer);
            var controller = SetupController(scope, freelancer.Id);
            var updateCertificationCommand = GetTestUpdateCertificationCommand(certification.Id);

            var result = await controller.Update(updateCertificationCommand);

            result.Result.ShouldBeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result.Result).Value.ShouldBeOfType(typeof(CertificationViewModel));
        }

        private static AddCertificationCommand GetTestAddCertificationCommand()
            => new(
                "Azure",
                "Microsoft",
                "Desc",
                DateTime.Now,
                DateTime.Now.AddMonths(5));

        private static UpdateCertificationCommand GetTestUpdateCertificationCommand(Guid certificationId)
            => new(
                certificationId,
                "name",
                "provider",
                "desc",
                DateTime.Now,
                DateTime.Now.AddMonths(5));

        private static async Task<Certification> CreateTestCertification(IServiceScope scope, Freelancer freelancer)
        {
            var context = scope.ServiceProvider.GetRequiredService<FreelancerProfileContext>();

            var certification = new Certification("name", "provider", new DateRange(DateTime.Now, DateTime.Now.AddMonths(1)), "desc");
            freelancer.AddCertification(certification);

            await context.SaveChangesAsync();

            return certification;
        }

    }
}
