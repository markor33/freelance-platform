using FluentValidation;
using JobManagement.Application.Commands;

namespace JobManagement.Application.Validations
{
    public class TerminateContractCommandValidator : AbstractValidator<TerminateContractCommand>
    {
        public TerminateContractCommandValidator()
        {
            RuleFor(x => x.JobId).NotEmpty();

            RuleFor(x => x.ContractId).NotEmpty();
        }
    }
}
