using FluentValidation;
using JobManagement.Application.Commands;

namespace JobManagement.Application.Validations
{
    public class ClientAcceptProposalCommandValidator : AbstractValidator<ClientAcceptProposalCommand>
    {
        public ClientAcceptProposalCommandValidator()
        {
            RuleFor(x => x.JobId).NotEmpty();

            RuleFor(x => x.ProposalId).NotEmpty();
        }
    }
}
