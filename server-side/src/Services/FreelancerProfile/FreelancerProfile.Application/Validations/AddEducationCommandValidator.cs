using FluentValidation;
using FreelancerProfile.Application.Commands;

namespace FreelancerProfile.Application.Validations
{
    public class AddEducationCommandValidator : AbstractValidator<AddEducationCommand>
    {
        public AddEducationCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.SchoolName).NotEmpty();

            RuleFor(x => x.Degree).NotEmpty();

            RuleFor(x => x.Start).NotEmpty();

            RuleFor(x => x.End).NotEmpty();
        }
    }
}
