using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entites;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class AddEducationCommandHandler : IRequestHandler<AddEducationCommand, Education>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public AddEducationCommandHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<Education> Handle(AddEducationCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByUserIdAsync(request.UserId);
            if (freelancer is null)
                return null;

            var attended = new DateRange(DateOnly.FromDateTime(request.Start), DateOnly.FromDateTime(request.End));
            var education = new Education(request.SchoolName, request.Degree, attended);
            freelancer.AddEducation(education);

            var result = await _freelancerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            if (result == 0)
                return null;
            return education;
        }
    }
}
