using GrpcJobManagement;

namespace Web.Bff.Services
{
    public interface IProposalService
    {
        Task<List<Models.Proposal>> GetProposalsByJobIdAsync(string jobId);
    }
}
