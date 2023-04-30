using FluentResults;

namespace JobManagement.Application.Queries
{
    public interface IProposalQueries
    {
        Task<List<ProposalViewModel>> GetByJobId(Guid jobId);
        Task<Result<ProposalViewModel>> GetByIdAsync(Guid id);
    }
}
