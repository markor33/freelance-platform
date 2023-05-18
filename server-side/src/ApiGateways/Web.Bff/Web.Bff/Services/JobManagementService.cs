using GrpcJobManagement;

namespace Web.Bff.Services
{
    public class JobManagementService : IJobManagementService
    {
        private readonly Job.JobClient _jobClient;

        public JobManagementService(Job.JobClient jobClient)
        {
            _jobClient = jobClient;
        }

        public async Task<JobDTO> GetById(string id)
        {
            var request = new GetJobBasicDataRequest() { Id = id.ToString() };
            return await _jobClient.GetJobBasicDataAsync(request);
        }
    }
}
