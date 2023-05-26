using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class EditEducationCommandHandler : IRequestHandler<EditEducationCommand, Result<Education>>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public EditEducationCommandHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<Result<Education>> Handle(EditEducationCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(request.FreelancerId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            var updateResult = freelancer.UpdateEducation(request.EducationId, request.SchoolName, request.Degree,
                new DateRange(request.Start, request.End));
            if (updateResult.IsFailed)
                return updateResult;

            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync();
            if (!result)
                return Result.Fail("Edit education action failed");

            return Result.Ok(updateResult.Value);
        }
    }
}
