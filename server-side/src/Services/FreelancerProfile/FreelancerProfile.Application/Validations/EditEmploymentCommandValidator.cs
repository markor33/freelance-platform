using FluentValidation;
using FreelancerProfile.Application.Commands;

namespace FreelancerProfile.Application.Validations
{
    public class EditEmploymentCommandValidator : AbstractValidator<UpdateEmploymentCommand>
    {
        public EditEmploymentCommandValidator()
        {
            RuleFor(x => x.FreelancerId).NotEmpty();

            RuleFor(x => x.EmploymentId).NotEmpty();

            RuleFor(x => x.Title).NotEmpty();

            RuleFor(x => x.Company).NotEmpty();

            RuleFor(x => x.Start).NotEmpty();

            RuleFor(x => x.End).NotEmpty();
        }
    }
}
