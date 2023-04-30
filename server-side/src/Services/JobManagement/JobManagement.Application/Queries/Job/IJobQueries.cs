namespace JobManagement.Application.Queries
{
    public interface IJobQueries
    {
        Task<List<JobViewModel>> GetAllAsync();
        Task<List<JobViewModel>> GetByClientAsync(Guid clientId);
    }
}
