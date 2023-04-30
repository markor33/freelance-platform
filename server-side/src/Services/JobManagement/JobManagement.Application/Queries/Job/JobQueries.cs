﻿using Dapper;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;
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
            var jobs = await _dbConnection.QueryAsync<JobViewModel, Payment, ProfessionViewModel, QuestionViewModel, SkillViewModel, JobViewModel>(
                @"SELECT j.""Id"", j.""Title"", j.""Description"", j.""ExperienceLevel"", 
                        j.""Payment_Amount"" as Amount, j.""Payment_Currency"" as Currency, j.""Payment_Type"" as Type, j.""Credits"",
                        p.""Id"", p.""Name"", p.""Description"",
                        q.""Id"", q.""Text"",
                        s.""Id"", s.""Name"", s.""Description""
                    FROM ""Jobs"" j 
                    INNER JOIN ""Professions"" p ON j.""ProfessionId"" = p.""Id""
                    LEFT JOIN ""Questions"" q ON j.""Id"" = q.""JobId""
                    LEFT JOIN ""JobSkill"" js ON j.""Id"" = js.""JobsId""
                    LEFT JOIN ""Skills"" s ON s.""Id"" = js.""SkillsId""
                    WHERE j.""JobStatus"" != 3",
                (job, payment, profession, question, skill) =>
                {
                    job.Profession = profession;
                    job.Payment = payment;
                    job.Questions.Add(question);
                    job.Skills.Add(skill);
                    return job;
                },
                splitOn: "Amount, Id, Id, Id");

            return GroupJobs(jobs);
        }

        public async Task<List<JobViewModel>> GetByClientAsync(Guid clientId)
        {
            var jobs = await _dbConnection.QueryAsync<JobViewModel, Payment, ProfessionViewModel, QuestionViewModel, SkillViewModel, JobViewModel>(
                @"SELECT j.""Id"", j.""Title"", j.""Description"", j.""ExperienceLevel"", 
                        j.""Payment_Amount"" as Amount, j.""Payment_Currency"" as Currency, j.""Payment_Type"" as Type, j.""Credits"",
                        p.""Id"", p.""Name"", p.""Description"",
                        q.""Id"", q.""Text"",
                        s.""Id"", s.""Name"", s.""Description""
                    FROM ""Jobs"" j 
                    INNER JOIN ""Professions"" p ON j.""ProfessionId"" = p.""Id""
                    LEFT JOIN ""Questions"" q ON j.""Id"" = q.""JobId""
                    LEFT JOIN ""JobSkill"" js ON j.""Id"" = js.""JobsId""
                    LEFT JOIN ""Skills"" s ON s.""Id"" = js.""SkillsId""
                    WHERE j.""JobStatus"" != 3 AND ""ClientId""=@clientId",
                (job, payment, profession, question, skill) =>
                {
                    job.Profession = profession;
                    job.Payment = payment;
                    job.Questions.Add(question);
                    job.Skills.Add(skill);
                    return job;
                },
                new { clientId },
                splitOn: "Amount, Id, Id, Id");

            return GroupJobs(jobs);
        }

        private static List<JobViewModel> GroupJobs(IEnumerable<JobViewModel> jobs)
        {
            var groupedJobs = jobs.GroupBy(
                job => job.Id,
                (key, group) => new JobViewModel(
                    key,
                    group.Select(job => job.Title).FirstOrDefault(),
                    group.Select(job => job.Description).FirstOrDefault(),
                    group.Select(job => job.ExperienceLevel).FirstOrDefault(),
                    group.Select(job => job.Payment).FirstOrDefault(),
                    group.Select(job => job.Credits).FirstOrDefault(),
                    group.SelectMany(job => job.Questions).Distinct().ToList(),
                    group.Select(job => job.Profession).FirstOrDefault(),
                    group.SelectMany(job => job.Skills).Distinct().ToList()));

            return groupedJobs.ToList();
        }
    }
}
