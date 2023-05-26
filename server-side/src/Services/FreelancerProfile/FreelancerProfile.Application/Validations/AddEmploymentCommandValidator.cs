using FluentValidation;
using FreelancerProfile.Application.Commands;
using System.Runtime.Serialization;

namespace FreelancerProfile.Application.Validations
{
    [DataContract]
    public class AddEmploymentCommandValidator : AbstractValidator<AddEmploymentCommand>
    {
        public AddEmploymentCommandValidator()
        {
            RuleFor(x => x.FreelancerId).NotEmpty();

            RuleFor(x => x.Title).NotEmpty();

            RuleFor(x => x.Company).NotEmpty();

            RuleFor(x => x.Start).NotEmpty();

            RuleFor(x => x.End).NotEmpty();
        }
    }
}
