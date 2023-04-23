using Dapper;
using System.Data;

namespace JobManagement.Application.Queries
{
    public class JobQueries : IJobQueries
    {
        private readonly IDbConnection _dbConnection;

        public JobQueries(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<JobViewModel>> GetAllAsync()
        {
            var jobs = await _dbConnection.QueryAsync<JobViewModel, QuestionViewModel, JobViewModel>(
                @"SELECT j.""Id"", j.""Title"", j.""Description"", j.""ExperienceLevel"", 
                        j.""Payment_Amount"", j.""Payment_Currency"", j.""Payment_Type"", j.""Credits"",
                        q.""Id"", q.""Text""
                    FROM ""Jobs"" j 
                    LEFT JOIN ""Questions"" q ON j.""Id"" = q.""JobId""
                ", (job, question) =>
                {
                    job.Questions.Add(question);
                    return job;
                });

            var groupedJobs = jobs.GroupBy(
                job => job.Id,
                (key, group) => new JobViewModel(
                    key,
                    group.Select(job => job.Title).FirstOrDefault(),
                    group.Select(job => job.Description).FirstOrDefault(),
                    group.Select(job => job.ExperienceLevel).FirstOrDefault(),
                    group.Select(job => job.Payment).FirstOrDefault(),
                    group.Select(job => job.Credits).FirstOrDefault(),
                    group.SelectMany(job => job.Questions).ToList()));

            return groupedJobs.ToList();
        }
    }
}
