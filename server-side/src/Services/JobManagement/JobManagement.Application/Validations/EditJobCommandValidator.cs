using FluentValidation;
using JobManagement.Application.Commands.JobCommands;

namespace JobManagement.Application.Validations
{
    public class EditJobCommandValidator : AbstractValidator<EditJobCommand>
    {
        public EditJobCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();

            RuleFor(x => x.Description).NotEmpty();

            RuleFor(x => x.ExperienceLevel).NotEmpty();

            RuleFor(x => x.Payment).NotEmpty();

            RuleFor(x => x.ProfessionId).NotEmpty();
        }
    }
}
