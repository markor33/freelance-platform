

using Dapper;
using FluentResults;
using JobManagement.Domain.AggregatesModel.JobAggregate.ValueObjects;
using System.Data;

namespace JobManagement.Application.Queries
{
    public class ProposalQueries : IProposalQueries
    {
        private readonly IDbConnection _dbConnection;

        public ProposalQueries(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Result<ProposalViewModel>> GetByIdAsync(Guid id)
        {
            var result = await _dbConnection.QueryAsync<ProposalViewModel, Payment, ProposalViewModel>(
                @"SELECT ""Id"", ""FreelancerId"", ""Text"", ""Payment_Amount"" as Amount, ""Payment_Currency"" as Currency, ""Payment_Type"" as Type, ""Status"" 
                    FROM ""Proposals""
                    WHERE ""Id""=@id",
                (proposal, payment) =>
                {
                    proposal.Payment = payment;
                    return proposal;
                },
                new { id },
                splitOn: "Amount");

            var proposal = result.FirstOrDefault();
            if (proposal is null)
                return Result.Fail("Proposal does not exist");
            return Result.Ok(proposal);
        }

        public async Task<List<ProposalViewModel>> GetByJobId(Guid jobId)
        {
            var proposals = await _dbConnection.QueryAsync<ProposalViewModel, Payment, ProposalViewModel>(
                @"SELECT ""Id"", ""FreelancerId"", ""Text"", ""Payment_Amount"" as Amount, ""Payment_Currency"" as Currency, ""Payment_Type"" as Type, ""Status"" 
                    FROM ""Proposals""
                    WHERE ""JobId""=@jobid",
                (proposal, payment) =>
                {
                    proposal.Payment = payment;
                    return proposal;
                },
                new { jobId },
                splitOn: "Amount");

            return proposals.ToList();
        }

    }
}
