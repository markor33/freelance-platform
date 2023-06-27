using FluentResults;
using GrpcFeedbackManagement;
using GrpcJobManagement;

namespace Web.Bff.Services
{
    public class ProposalService : IProposalService
    {
        private readonly Proposal.ProposalClient _proposalClient;
        private readonly IFeedbackService _feedbackService;
        private readonly IFreelancerProfileService _freelancerProfileService;

        public ProposalService(
            Proposal.ProposalClient proposalClient,
            IFeedbackService feedbackService,
            IFreelancerProfileService freelancerProfileService)
        {
            _proposalClient = proposalClient;
            _feedbackService = feedbackService;    
            _freelancerProfileService = freelancerProfileService;
        }

        public async Task<List<Models.Proposal>> GetProposalsByJobIdAsync(string jobId)
        {
            var response = await _proposalClient.GetProposalsByJobIdAsync(new GetProposalsByJobIdRequest() { JobId = jobId });
            var proposals = new List<Models.Proposal>();
            foreach (var proposal in response.Proposals)
            {
                var freelancer = await _freelancerProfileService.GetBasicDataByIdAsync(proposal.FreelancerId);
                var averageRating = await _feedbackService.GetFreelancerAverageRating(proposal.FreelancerId);
                proposals.Add(new Models.Proposal(proposal, freelancer, averageRating));
            }
            return proposals;
        }
    }
}
