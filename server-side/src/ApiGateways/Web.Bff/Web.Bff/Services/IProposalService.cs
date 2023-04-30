using GrpcJobManagement;

namespace Web.Bff.Services
{
    public interface IProposalService
    {
        Task<List<ProposalDTO>> GetProposalsByJobIdAsync(string jobId);
    }
}
