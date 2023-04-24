using Dapper;
using FluentResults;
using System.Data;

namespace FreelancerProfile.Application.Queries
{
    public class LanguageQueries : ILanguageQueries
    {
        private readonly IDbConnection _dbConnection;

        public LanguageQueries(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<LanguageViewModel>> GetAllAsync()
        {
            var languages = await _dbConnection.QueryAsync<LanguageViewModel>(
                @"SELECT ""Id"", ""Name"", ""ShortName""
                    FROM ""Languages""");

            return languages.ToList();
        }

        public async Task<Result<LanguageViewModel>> GetByIdAsync(int id)
        {
            var lanuage = await _dbConnection.QueryFirstAsync<LanguageViewModel>(
                @"SELECT ""Id"", ""Name"", ""ShortName""
                    FROM ""Languages""
                    WHERE ""Id""=@id"
                , new { id });

            if (lanuage is null)
                return Result.Fail("Language does not exist");
            return Result.Ok(lanuage);
        }
    }
}
