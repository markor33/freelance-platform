using FluentValidation;
using FreelancerProfile.Application.Commands;

namespace FreelancerProfile.Application.Validations
{
    public class DeleteEmploymentCommandValidator : AbstractValidator<DeleteEmploymentCommand>
    {
        public DeleteEmploymentCommandValidator()
        {
            RuleFor(x => x.FreelancerId).NotEmpty();

            RuleFor(x => x.EmploymentId).NotEmpty();
        }
    }
}
