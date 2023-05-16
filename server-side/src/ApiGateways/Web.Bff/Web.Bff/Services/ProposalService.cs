using GrpcJobManagement;

namespace Web.Bff.Services
{
    public class ProposalService : IProposalService
    {
        private readonly Proposal.ProposalClient _proposalClient;

        public ProposalService(Proposal.ProposalClient proposalClient)
        {
            _proposalClient = proposalClient;
        }

        public async Task<List<ProposalDTO>> GetProposalsByJobIdAsync(string jobId)
        {
            var response = await _proposalClient.GetProposalsByJobIdAsync(new GetProposalsByJobIdRequest() { JobId = jobId });
            return response.Proposals.ToList();
        }
    }
}
