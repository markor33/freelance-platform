using Dapper;
using JobManagement.Domain.AggregatesModel.JobAggregate.Enums;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;
using System.Data;

namespace JobManagement.Application.Queries
{
    public class JobQueries : IJobQueries
    {
        private readonly IDbConnection _dbConnection;
        private readonly IProposalQueries _proposalQueries;
        private readonly IContractQueries _contractQueries;

        public JobQueries(
            IDbConnection dbConnection, 
            IProposalQueries proposalQueries,
            IContractQueries contractQueries)
        {
            _dbConnection = dbConnection;
            _proposalQueries = proposalQueries;
            _contractQueries = contractQueries;
        }

        public async Task<List<JobViewModel>> GetAllAsync()
        {
            var jobs = await _dbConnection.QueryAsync<JobViewModel, Payment, ProfessionViewModel, QuestionViewModel, SkillViewModel, JobViewModel>(
                @"SELECT j.""Id"", j.""ClientId"", j.""Title"", j.""Description"", j.""ExperienceLevel"", j.""Credits"", j.""Status"",
                        j.""Payment_Amount"" as Amount, j.""Payment_Currency"" as Currency, j.""Payment_Type"" as Type,
                        p.""Id"", p.""Name"", p.""Description"",
                        q.""Id"", q.""Text"",
                        s.""Id"", s.""Name"", s.""Description""
                    FROM ""Jobs"" j 
                    INNER JOIN ""Professions"" p ON j.""ProfessionId"" = p.""Id""
                    LEFT JOIN ""Questions"" q ON j.""Id"" = q.""JobId""
                    LEFT JOIN ""JobSkill"" js ON j.""Id"" = js.""JobsId""
                    LEFT JOIN ""Skills"" s ON s.""Id"" = js.""SkillsId""
                    WHERE j.""Status"" != 3",
                (job, payment, profession, question, skill) =>
                {
                    job.Profession = profession;
                    job.Payment = payment;
                    job.Questions.Add(question);
                    job.Skills.Add(skill);
                    return job;
                },
                splitOn: "Amount, Id, Id, Id");

            return await GroupJobs(jobs);
        }

        public async Task<JobViewModel> GetByIdAsync(Guid id)
        {
            var jobs = await _dbConnection.QueryAsync<JobViewModel, Payment, ProfessionViewModel, QuestionViewModel, SkillViewModel, JobViewModel>(
                @"SELECT j.""Id"", j.""ClientId"", j.""Title"", j.""Description"", j.""ExperienceLevel"", j.""Credits"", j.""Status"",
                        j.""Payment_Amount"" as Amount, j.""Payment_Currency"" as Currency, j.""Payment_Type"" as Type,
                        p.""Id"", p.""Name"", p.""Description"",
                        q.""Id"", q.""Text"",
                        s.""Id"", s.""Name"", s.""Description""
                    FROM ""Jobs"" j 
                    INNER JOIN ""Professions"" p ON j.""ProfessionId"" = p.""Id""
                    LEFT JOIN ""Questions"" q ON j.""Id"" = q.""JobId""
                    LEFT JOIN ""JobSkill"" js ON j.""Id"" = js.""JobsId""
                    LEFT JOIN ""Skills"" s ON s.""Id"" = js.""SkillsId""
                    WHERE j.""Status"" != 3 AND j.""Id""=@id",
                (job, payment, profession, question, skill) =>
                {
                    job.Profession = profession;
                    job.Payment = payment;
                    job.Questions.Add(question);
                    job.Skills.Add(skill);
                    return job;
                },
                new { id },
                splitOn: "Amount, Id, Id, Id");

            return (await GroupJobs(jobs)).First();
        }

        public async Task<List<JobViewModel>> GetByClientAsync(Guid clientId)
        {
            var jobs = await _dbConnection.QueryAsync<JobViewModel, Payment, ProfessionViewModel, QuestionViewModel, SkillViewModel, JobViewModel>(
                @"SELECT j.""Id"", j.""ClientId"", j.""Title"", j.""Description"", j.""ExperienceLevel"", j.""Credits"", j.""Status"",
                        j.""Payment_Amount"" as Amount, j.""Payment_Currency"" as Currency, j.""Payment_Type"" as Type,
                        p.""Id"", p.""Name"", p.""Description"",
                        q.""Id"", q.""Text"",
                        s.""Id"", s.""Name"", s.""Description""
                    FROM ""Jobs"" j 
                    INNER JOIN ""Professions"" p ON j.""ProfessionId"" = p.""Id""
                    LEFT JOIN ""Questions"" q ON j.""Id"" = q.""JobId""
                    LEFT JOIN ""JobSkill"" js ON j.""Id"" = js.""JobsId""
                    LEFT JOIN ""Skills"" s ON s.""Id"" = js.""SkillsId""
                    WHERE j.""Status"" != 3 AND ""ClientId""=@clientId",
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

            return await GroupJobs(jobs);
        }

        private async Task<List<JobViewModel>> GroupJobs(IEnumerable<JobViewModel> jobs)
        {
            var groupedJobs = jobs.GroupBy(
                job => job.Id,
                (key, group) => new JobViewModel(
                    key,
                    group.Select(job => job.ClientId).FirstOrDefault(),
                    group.Select(job => job.Title).FirstOrDefault(),
                    group.Select(job => job.Description).FirstOrDefault(),
                    group.Select(job => job.ExperienceLevel).FirstOrDefault(),
                    group.Select(job => job.Status).FirstOrDefault(),
                    group.Select(job => job.Payment).FirstOrDefault(),
                    group.Select(job => job.Credits).FirstOrDefault(),
                    group.SelectMany(job => job.Questions).Distinct().ToList(),
                    group.Select(job => job.Profession).FirstOrDefault(),
                    group.SelectMany(job => job.Skills).Distinct().ToList())).ToList();

            for (int i = 0; i < groupedJobs.Count; i++)
                await CountProposals(groupedJobs[i]);
            for (int i = 0; i < groupedJobs.Count; i++)
                groupedJobs[i].NumOfActiveContracts = await _contractQueries.GetNumOfActiveContracts(groupedJobs[i].Id);

            return groupedJobs;
        }

        private async Task<JobViewModel> CountProposals(JobViewModel job)
        {
            var proposals = await _proposalQueries.GetByJobId(job.Id);
            job.NumOfProposals = proposals.Count;
            job.CurrentlyInterviewing = proposals
                .Where(p => p.Status == ProposalStatus.INTERVIEW || p.Status == ProposalStatus.CLIENT_APPROVED).Count();
            return job;
        }
    }
}
