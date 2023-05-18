using GrpcJobManagement;

namespace Web.Bff.Services
{
    public interface IJobManagementService
    {
        Task<JobDTO> GetById(string id);
    }
}
