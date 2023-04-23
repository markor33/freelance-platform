using Dapper;
using FluentResults;
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
            var proposal = await _dbConnection.QueryFirstAsync<ProposalViewModel>(
                @"SELECT ""Id"", ""Text"", ""Payment_Amount"", ""Payment_Currency"", ""Payment_Type"", ""ProposalStatus"" 
                    FROM ""Proposals""
                    WHERE ""Id""=@id"
                , new { id });

            if (proposal is null)
                return Result.Fail("Proposal does not exist");
            return Result.Ok(proposal);
        }
    }
}
