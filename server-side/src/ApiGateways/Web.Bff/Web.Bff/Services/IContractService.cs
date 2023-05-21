namespace Web.Bff.Services
{
    public interface IContractService
    {
        Task<List<Models.Contract>> GetByClient(Guid clientId);
        Task<List<Models.Contract>> GetByJob(Guid jobId);
    }
}
