using Google.Protobuf.WellKnownTypes;
using GrpcFeedbackManagement;
using GrpcJobManagement;
using Web.Bff.Models;

namespace Web.Bff.Services
{
    public class JobManagementService : IJobManagementService
    {
        private readonly Job.JobClient _jobClient;
        private readonly IClientProfileService _clientProfileService;
        private readonly GrpcFeedbackManagement.Feedback.FeedbackClient _feedbackClient;

        public JobManagementService(
            Job.JobClient jobClient, 
            IClientProfileService clientProfileService,
            GrpcFeedbackManagement.Feedback.FeedbackClient feedbackClient)
        {
            _jobClient = jobClient;
            _clientProfileService = clientProfileService;
            _feedbackClient = feedbackClient;
        }

        public async Task<JobDTO> GetById(string id)
        {
            var request = new GetJobBasicDataRequest() { Id = id.ToString() };
            return await _jobClient.GetJobBasicDataAsync(request);
        }

        public async Task<List<SearchJob>> Search(string? queryText, JobSearchFilters? filters)
        {
            var request = new SearchJobsRequest();
            request.QueryText = queryText ?? string.Empty;
            request.Filters = new SearchJobFilters();
            request.Filters.Professions.AddRange(filters.Professions.Select(x => x.ToString()).ToList());
            request.Filters.Experiences.AddRange(filters.ExperienceLevels.Select(x => (int)x).ToList());
            request.Filters.Payments.AddRange(filters.PaymentTypes.Select(x => (int)x).ToList());

            var response = await _jobClient.SearchJobsAsync(request);
            var jobs = new List<SearchJob>();
            foreach (var jobDTO in response.Jobs)
            {
                var client = await _clientProfileService.GetBasicDataByIdAsync(jobDTO.ClientId);
                var clientAverageRating = await _feedbackClient.GetAverageClientRatingAsync(new GetAverageClientRatingRequest() { ClientId = jobDTO.ClientId });
                jobs.Add(new SearchJob(jobDTO, client, clientAverageRating.AverageRating));
            }

            return jobs;
        }
    }
}
