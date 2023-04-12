using FluentValidation;
using FreelancerProfile.Application.Commands;

namespace FreelancerProfile.Application.Validations
{
    public class AddCertificationCommandValidator : AbstractValidator<AddCertificationCommand>
    {
        public AddCertificationCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.Name).NotEmpty();
            
            RuleFor(x => x.Provider).NotEmpty();

            RuleFor(x => x.Start).NotEmpty();

            RuleFor(x => x.End).NotEmpty();
        }
    }
}
