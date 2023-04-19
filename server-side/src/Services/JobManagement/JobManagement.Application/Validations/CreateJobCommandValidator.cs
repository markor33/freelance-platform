using FluentValidation;
using JobManagement.Application.Commands;

namespace JobManagement.Application.Validations
{
    public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
    {
        public CreateJobCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(X => X.Title).NotEmpty();

            RuleFor(X => X.Description).NotEmpty();
        }

    }
}
