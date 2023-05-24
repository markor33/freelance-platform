using GrpcJobManagement;
using Web.Bff.Models;

namespace Web.Bff.Services
{
    public interface IJobManagementService
    {
        Task<List<SearchJob>> Search(string queryText, JobSearchFilters filters);
        Task<JobDTO> GetById(string id);
    }
}
