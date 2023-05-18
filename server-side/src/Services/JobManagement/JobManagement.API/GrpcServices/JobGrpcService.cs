using Grpc.Core;
using GrpcJobManagement;
using JobManagement.Application.Queries;

namespace JobManagement.API.GrpcServices
{
    public class JobGrpcService : Job.JobBase
    {
        private readonly IJobQueries _jobQueries;

        public JobGrpcService(IJobQueries jobQueries)
        {
            _jobQueries = jobQueries;
        }

        public async override Task<JobDTO> GetJobBasicData(GetJobBasicDataRequest request, ServerCallContext context)
        {
            var job = await _jobQueries.GetByIdAsync(Guid.Parse(request.Id));
            var jobDto = new JobDTO()
            {
                Id = job.Id.ToString(),
                Title = job.Title,
            };
            return jobDto;
        }
    }
}
