using EventBus.Events;
using FreelancerProfile.Application.IntegrationEvents;
using IntegrationEventLog.EFCore.Services;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace FreelancerProfile.Infrastructure.Persistence.Services
{
    public class FreelancerProfileIntegrationEventService : IFreelancerProfileIntegrationEventService
    {
        private readonly FreelancerProfileContext _context;
        private readonly IIntegrationEventLogService _integrationEventLogService;

        public FreelancerProfileIntegrationEventService(
            FreelancerProfileContext context,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _context = context;
            _integrationEventLogService = integrationEventLogServiceFactory(context.Database.GetDbConnection());
        }

        public async Task SaveEventAsync(IntegrationEvent @event)
        {
            await _integrationEventLogService.SaveEventAsync(@event, _context.GetCurrentTransaction());
        }
    }
}
