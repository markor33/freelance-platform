using FluentResults;

namespace JobManagement.Application.Queries
{
    public interface IProposalQueries
    {
        Task<Result<ProposalViewModel>> GetByIdAsync(Guid id);
    }
}
