namespace JobManagement.Application.Services
{
    public interface IClientService
    {
        Task<Guid> GetClientIdByUserId(Guid userId);
    }
}
