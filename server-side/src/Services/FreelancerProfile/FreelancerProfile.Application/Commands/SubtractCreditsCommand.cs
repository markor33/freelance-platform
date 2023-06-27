using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class SubtractCreditsCommand : IRequest<Unit>
    {
        public Guid FreelancerId { get; private set; }
        public Guid JobId { get; private set; }
        public Guid ProposalId { get; private set; }
        public int Credits { get; private set; }

        public SubtractCreditsCommand(Guid freelancerId, Guid jobId, Guid proposalId, int credits)
        {
            FreelancerId = freelancerId;
            JobId = jobId;
            ProposalId = proposalId;
            Credits = credits;
        }

    }
}
