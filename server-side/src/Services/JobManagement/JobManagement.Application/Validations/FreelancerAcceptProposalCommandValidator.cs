using FluentValidation;
using JobManagement.Application.Commands;

namespace JobManagement.Application.Validations
{
    public class FreelancerAcceptProposalCommandValidator : AbstractValidator<FreelancerAcceptProposalCommand>
    {
        public FreelancerAcceptProposalCommandValidator()
        {
            RuleFor(x => x.JobId);

            RuleFor(x => x.ProposalId);
        }
    }
}
