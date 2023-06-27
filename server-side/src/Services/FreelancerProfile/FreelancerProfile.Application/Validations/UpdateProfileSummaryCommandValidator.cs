using FluentValidation;
using FreelancerProfile.Application.Commands;

namespace FreelancerProfile.Application.Validations
{
    public class UpdateProfileSummaryCommandValidator : AbstractValidator<UpdateProfileSummaryCommand>
    {
        public UpdateProfileSummaryCommandValidator()
        {
            RuleFor(x => x.FreelancerId).NotEmpty();

            RuleFor(x => x.ProfileSummary).NotEmpty();
        }
    }
}
