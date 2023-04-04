using FluentValidation;
using FreelancerProfile.Application.Commands;

namespace FreelancerProfile.Application.Validations
{
    public class CreateFreelancerCommandValidator : AbstractValidator<CreateFreelancerCommand>
    {
        public CreateFreelancerCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);

            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);

            RuleFor(x => x.Country).NotEmpty().MaximumLength(100);

            RuleFor(x => x.City).NotEmpty().MaximumLength(100);

            RuleFor(x => x.Street).NotEmpty().MaximumLength(100);

            RuleFor(x => x.Number).NotEmpty().MaximumLength(25);

            RuleFor(x => x.ZipCode).NotEmpty().MaximumLength(25);

            RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(50);

            RuleFor(x => x.TimeZoneId).NotEmpty().MaximumLength(50);
        }
    }
}
