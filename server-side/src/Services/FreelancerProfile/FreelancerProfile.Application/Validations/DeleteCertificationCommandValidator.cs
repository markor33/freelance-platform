using FluentValidation;
using FreelancerProfile.Application.Commands;

namespace FreelancerProfile.Application.Validations
{
    public class DeleteCertificationCommandValidator : AbstractValidator<DeleteCertificationCommand>
    {
        public DeleteCertificationCommandValidator()
        {
            RuleFor(x => x.FreelancerId).NotEmpty();

            RuleFor(x => x.CertificationId).NotEmpty();
        }
    }
}
