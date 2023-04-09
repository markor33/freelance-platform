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

            RuleFor(x => x.Contact.Address.Country).NotEmpty().MaximumLength(100);

            RuleFor(x => x.Contact.Address.City).NotEmpty().MaximumLength(100);

            RuleFor(x => x.Contact.Address.Street).NotEmpty().MaximumLength(100);

            RuleFor(x => x.Contact.Address.Number).NotEmpty().MaximumLength(25);

            RuleFor(x => x.Contact.Address.ZipCode).NotEmpty().MaximumLength(25);

            RuleFor(x => x.Contact.PhoneNumber).NotEmpty().MaximumLength(50);

            RuleFor(x => x.Contact.TimeZoneId).NotEmpty().MaximumLength(50);

            RuleFor(x => x.IsProfilePublic).NotEmpty();

            RuleFor(x => x.ProfileSummary.Title).NotEmpty().MaximumLength(50);

            RuleFor(x => x.ProfileSummary.Description).NotEmpty();

            RuleFor(x => x.HourlyRate.Amount).NotEmpty().GreaterThan(0);

            RuleFor(x => x.HourlyRate.Currency).NotEmpty().MaximumLength(5);

            RuleFor(x => x.Availability).IsInEnum();

            RuleFor(x => x.ExperienceLevel).IsInEnum();

            RuleFor(x => x.ProfessionId).NotEmpty();

            RuleFor(x => x.LanguageId).NotEmpty();

            RuleFor(x => x.LanguageProficiencyLevel).IsInEnum();
        }
    }
}
