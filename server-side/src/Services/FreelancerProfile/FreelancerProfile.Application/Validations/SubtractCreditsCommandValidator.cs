using FluentValidation;
using FreelancerProfile.Application.Commands;

namespace FreelancerProfile.Application.Validations
{
    public class SubtractCreditsCommandValidator : AbstractValidator<SubtractCreditsCommand>
    {
        public SubtractCreditsCommandValidator()
        {
            RuleFor(x => x.FreelancerId).NotEmpty();

            RuleFor(x => x.JobId).NotEmpty();

            RuleFor(x => x.ProposalId).NotEmpty();

            RuleFor(x => x.Credits).NotEmpty();
        }
    }
}
