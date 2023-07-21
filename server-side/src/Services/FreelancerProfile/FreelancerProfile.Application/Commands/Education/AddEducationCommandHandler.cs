using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using FreelancerProfile.Domain.Repositories;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class AddEducationCommandHandler : IRequestHandler<AddEducationCommand, Result<Education>>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public AddEducationCommandHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<Result<Education>> Handle(AddEducationCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(request.FreelancerId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            var attended = new DateRange(request.Start, request.End);
            var education = new Education(request.SchoolName, request.Degree, attended);
            freelancer.AddEducation(education);

            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!result)
                return Result.Fail("Education creation failed");
            return Result.Ok(education);
        }
    }
}
