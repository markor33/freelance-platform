using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand, Result<Education>>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public UpdateEducationCommandHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<Result<Education>> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(request.FreelancerId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            freelancer.UpdateEducation(request.EducationId, request.SchoolName, request.Degree, new DateRange(request.Start, request.End));

            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync();
            if (!result)
                return Result.Fail("Edit education action failed");

            return Result.Ok();
        }
    }
}
