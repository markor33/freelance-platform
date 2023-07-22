namespace JobManagement.Infrastructure.EventStore
{
    public interface IEventStore
    {
        Task SaveEventsAsync(JobManagementContext context);
    }
}
