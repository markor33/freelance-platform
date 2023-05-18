using EventBus.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NotiftChat.IntegrationTests.Setup;
using NotifyChat.SignalR.Controllers;
using NotifyChat.SignalR.Hubs;
using NotifyChat.SignalR.Models;
using NotifyChat.SignalR.Persistence.Repositories;
using NotifyChat.SignalR.Security;
using Shouldly;
using Xunit;

namespace NotiftChat.IntegrationTests.Controllers
{
    public class ChatScenarios : BaseIntegrationTest
    {
        public ChatScenarios(TestDatabaseFactory factory) : base(factory)
        {

        }

        private static ChatController SetupController(IServiceScope scope)
        {
            var chatRepository = scope.ServiceProvider.GetRequiredService<IChatRepository>();
            var messageRepository = scope.ServiceProvider.GetRequiredService<IMessageRepository>();
            var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
            var chatHub = scope.ServiceProvider.GetRequiredService<IHubContext<ChatHub>>();
            var identityService = new Mock<IIdentityService>();
            identityService.Setup(i => i.GetUserId()).Returns(Guid.NewGuid());
            return new ChatController(chatRepository, messageRepository, chatHub, eventBus, identityService.Object);
        }

        [Fact]
        public async Task Create_Chat_ReturnsOk()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupController(scope);
            var createRequest = new CreateChatRequest()
            {
                JobId = Guid.NewGuid(),
                ProposalId = Guid.NewGuid(),
                FreelancerId = Guid.NewGuid(),
                InitialMessage = "message"
            };

            var result = await controller.Create(createRequest);

            result.Result.ShouldBeOfType(typeof(OkObjectResult));
            ((OkObjectResult)result.Result).Value.ShouldBeOfType(typeof(Chat));
        }
    }
}
