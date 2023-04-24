using EventBus.Abstractions;
using FreelancerProfile.Application.IntegrationEvents.Events;
using FreelancerProfile.Application.IntegrationEvents.Handlers;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.SeedWork;
using Moq;
using Shouldly;
using Xunit;

namespace FreelancerProfile.UnitTests.IntegrationEvents
{
    public class ProposalCreatedIntegrationEventHandlerTests
    {
        private readonly Mock<IFreelancerRepository> _mockFreelancerRepository;
        private readonly Mock<IEventBus> _mockEventBus;
        private readonly ProposalCreatedIntegrationEventHandler _handler;

        public ProposalCreatedIntegrationEventHandlerTests()
        {
            _mockFreelancerRepository = new Mock<IFreelancerRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            _mockFreelancerRepository.Setup(x => x.UnitOfWork).Returns(mockUnitOfWork.Object);
            _mockEventBus = new Mock<IEventBus>();
            _handler = new ProposalCreatedIntegrationEventHandler(_mockFreelancerRepository.Object, _mockEventBus.Object);
        }

        [Fact]
        public async Task HandleAsync_ShouldSubtractCreditsFromFreelancer()
        {
            // Arrange
            var @event = new ProposalCreatedIntegrationEvent(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1);
            var freelancer = new Freelancer();
            var oldCredits = freelancer.Credits;
            _mockFreelancerRepository.Setup(x => x.GetByIdAsync(@event.FreelancerId)).ReturnsAsync(freelancer);

            // Act
            await _handler.HandleAsync(@event);

            // Assert
            freelancer.Credits.ShouldBe(oldCredits - 1);
        }

        [Fact]
        public async Task HandleAsync_ShouldPublishCreditsReservedIntegrationEvent()
        {
            // Arrange
            var @event = new ProposalCreatedIntegrationEvent(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1);
            _mockFreelancerRepository.Setup(x => x.GetByIdAsync(@event.FreelancerId)).ReturnsAsync(new Freelancer());

            // Act
            await _handler.HandleAsync(@event);

            // Assert
            _mockEventBus.Verify(x => x.Publish(It.Is<CreditsReservedIntegrationEvent>(e => e.JobId == @event.JobId && e.ProposalId == @event.ProposalId)), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_ShouldPublishCreditsLimitExceededIntegrationEvent_WhenCreditsNotEnough()
        {
            // Arrange
            var @event = new ProposalCreatedIntegrationEvent(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 100);
            var freelancer = new Freelancer();
            _mockFreelancerRepository.Setup(x => x.GetByIdAsync(@event.FreelancerId)).ReturnsAsync(freelancer);

            // Act
            await _handler.HandleAsync(@event);

            // Assert
            _mockEventBus.Verify(x => x.Publish(It.Is<CreditsLimitExceededIntegrationEvent>(e => e.JobId == @event.JobId && e.ProposalId == @event.ProposalId)), Times.Once);
        }

    }
}
