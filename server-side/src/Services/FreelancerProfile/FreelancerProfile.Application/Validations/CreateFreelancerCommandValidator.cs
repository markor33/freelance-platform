using FluentValidation;
using FreelancerProfile.Application.Commands;

namespace FreelancerProfile.Application.Validations
{
    public class CreateFreelancerCommandValidator : AbstractValidator<CreateFreelancerCommand>
    {
        public CreateFreelancerCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.FirstName).NotEmpty();

            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.Contact).NotNull();
        }
    }
}
