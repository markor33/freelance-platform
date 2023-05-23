using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcJobManagement;
using JobManagement.Application.Queries;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;

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

        public override async Task<SearchJobsResponse> SearchJobs(SearchJobsRequest request, ServerCallContext context)
        {
            var filters = new JobSearchFilters();
            if (!string.IsNullOrEmpty(request.Filters.ProfessionId)) filters.ProfessionId = Guid.Parse(request.Filters.ProfessionId);
            if (request.Filters.HasExperience) filters.ExperienceLevel = (ExperienceLevel)request.Filters.Experience;
            if (request.Filters.HasPayment) filters.PaymentType = (PaymentType)request.Filters.Payment;

            var jobs = await _jobQueries.Search(request.QueryText, filters);
            var response = new SearchJobsResponse();
            foreach (var job in jobs)
                response.Jobs.Add(new JobDTO()
                {
                    Id = job.Id.ToString(),
                    ClientId = job.ClientId.ToString(),
                    Title = job.Title,
                    Description = job.Description,
                    Created = Timestamp.FromDateTime(job.Created),
                    Payment = new Payment()
                    {
                        Amount = job.Payment.Amount,
                        Currency = job.Payment.Currency,
                        Type = (int)job.Payment.Type
                    },
                    ExperienceLevel = (int)job.ExperienceLevel,
                    NumOfProposals = job.NumOfProposals,
                    CurrentlyInterviewing = job.CurrentlyInterviewing,
                });

            return response;
        }

    }
}
