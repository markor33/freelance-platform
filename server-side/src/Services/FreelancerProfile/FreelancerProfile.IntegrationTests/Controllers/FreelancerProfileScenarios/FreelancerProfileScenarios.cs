using AutoMapper;
using FreelancerProfile.API.Controllers;
using FreelancerProfile.API.Security;
using FreelancerProfile.Application.Queries;
using FreelancerProfile.IntegrationTests.Setup;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace FreelancerProfile.IntegrationTests.Controllers.FreelancerProfileScenarios
{
    [Collection("FreelancerProfileScenarios")]
    public partial class FreelancerProfileScenarios : BaseIntegrationTest
    {

        public FreelancerProfileScenarios(TestDatabaseFactory factory) : base(factory) { }

        private static FreelancerController SetupController(IServiceScope scope, Guid userId)
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var freelancerQueries = scope.ServiceProvider.GetRequiredService<IFreelancerQueries>();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            var identityService = new Mock<IIdentityService>();
            identityService.Setup(i => i.GetUserId()).Returns(userId);
            return new FreelancerController(mediator, freelancerQueries, identityService.Object, mapper);
        }

    }
}
