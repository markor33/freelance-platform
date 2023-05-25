using FluentValidation;
using FreelancerProfile.Application.Commands;

namespace FreelancerProfile.Application.Validations
{
    public class EditCertificationCommandValidator : AbstractValidator<EditCertificationCommand>
    {
        public EditCertificationCommandValidator()
        {
            RuleFor(x => x.FreelancerId).NotEmpty();

            RuleFor(x => x.CertificationId).NotEmpty();

            RuleFor(x => x.Name).NotEmpty();

            RuleFor(x => x.Provider).NotEmpty();

            RuleFor(x => x.Start).NotEmpty();

            RuleFor(x => x.End).NotEmpty();
        }
    }
}
