using Dapper;
using FluentResults;
using System.Data;

namespace FreelancerProfile.Application.Queries
{
    public class ProfessionQueries : IProfessionQueries
    {
        private readonly IDbConnection _dbConnection;

        public ProfessionQueries(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<ProfessionViewModel>> GetAllAsync()
        {
            var professions = await _dbConnection.QueryAsync<ProfessionViewModel>(
                @"SELECT ""Id"", ""Name"", ""Description""
                    FROM ""Professions""");

            return professions.ToList();
        }

        public async Task<Result<ProfessionViewModel>> GetByIdAsync(Guid id)
        {
            var profession = await _dbConnection.QueryFirstAsync<ProfessionViewModel>(
                @"SELECT ""Id"", ""Name"", ""Description""
                    FROM ""Professions""
                    WHERE ""Id""=@id"
                , new { id });

            if (profession is null)
                return Result.Fail("Profession does not exist");
            return Result.Ok(profession);
        }

        public async Task<Result<SkillViewModel>> GetSkillByIdAsync(Guid id)
        {
            var skill = await _dbConnection.QueryFirstAsync<SkillViewModel>(
                @"SELCT ""Id"", ""Name"", ""Description""
                    FROM ""Skills""
                    WHERE ""Id""=@id"
                , new { id });

            if (skill is null)
                return Result.Fail("Skill does not exist");
            return Result.Ok(skill);
        }

        public async Task<List<SkillViewModel>> GetSkillsByProfessionAsync(Guid id)
        {
            var skills = await _dbConnection.QueryAsync<SkillViewModel>(
                @"SELECT ""Id"", ""Name"", ""Description""
                    FROM ""Skills""
                    WHERE ""ProfessionId""=@id"
                , new { id });

            return skills.ToList();
        }
    }
}
