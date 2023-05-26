using FluentValidation;
using FreelancerProfile.Application.Commands;

namespace FreelancerProfile.Application.Validations
{
    public class EditEducationCommandValidator : AbstractValidator<UpdateEducationCommand>
    {
        public EditEducationCommandValidator()
        {
            RuleFor(x => x.FreelancerId).NotEmpty();

            RuleFor(x => x.EducationId).NotEmpty();

            RuleFor(x => x.SchoolName).NotEmpty();

            RuleFor(x => x.Degree).NotEmpty();

            RuleFor(x => x.Start).NotEmpty();

            RuleFor(x => x.End).NotEmpty();
        }
    }
}
