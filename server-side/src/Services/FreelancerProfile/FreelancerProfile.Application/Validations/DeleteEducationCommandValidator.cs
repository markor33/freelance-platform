using FluentValidation;
using FreelancerProfile.Application.Commands;

namespace FreelancerProfile.Application.Validations
{
    public class DeleteEducationCommandValidator : AbstractValidator<DeleteEducationCommand>
    {
        public DeleteEducationCommandValidator()
        {
            RuleFor(x => x.FreelancerId).NotEmpty();

            RuleFor(x => x.EducationId).NotEmpty();
        }
    }
}
