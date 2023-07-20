using FluentValidation;
using FreelancerProfile.Application.Commands;

namespace FreelancerProfile.Application.Validations
{
    public class ProfileSetupCommandValidator : AbstractValidator<ProfileSetupCommand>
    {
        public ProfileSetupCommandValidator()
        {
            RuleFor(x => x.FreelancerId).NotEmpty();

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
