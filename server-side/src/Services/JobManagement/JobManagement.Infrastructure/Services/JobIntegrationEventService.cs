using EventBus.Events;
using JobManagement.Application.IntegrationEvents;
using IntegrationEventLog.EFCore.Services;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace JobManagement.Infrastructure.Persistence.Services
{
    public class JobIntegrationEventService : IJobIntegrationEventService
    {
        private readonly JobManagementContext _context;
        private readonly IIntegrationEventLogService _integrationEventLogService;

        public JobIntegrationEventService(
            JobManagementContext context,
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
