namespace JobManagement.Application.Queries
{
    public interface IJobQueries
    {
        Task<List<JobViewModel>> GetAllAsync();
        Task<JobViewModel> GetByIdAsync(Guid id);
        Task<List<JobViewModel>> GetByClientAsync(Guid clientId);
    }
}
