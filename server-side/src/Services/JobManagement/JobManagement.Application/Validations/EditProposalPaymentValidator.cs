using FluentValidation;
using JobManagement.Application.Commands;

namespace JobManagement.Application.Validations
{
    public class EditProposalPaymentValidator : AbstractValidator<EditProposalPaymentCommand>
    {
        public EditProposalPaymentValidator()
        {
            RuleFor(x => x.JobId).NotEmpty();

            RuleFor(x => x.ProposalId).NotEmpty();

            RuleFor(x => x.Payment).NotEmpty();
        }
    }
}
