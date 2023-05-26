using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class UpdateProfileSummaryCommandHandler : IRequestHandler<UpdateProfileSummaryCommand, Result<ProfileSummary>>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public UpdateProfileSummaryCommandHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<Result<ProfileSummary>> Handle(UpdateProfileSummaryCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(request.FreelancerId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            freelancer.UpdateProfileSummary(request.ProfileSummary);

            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync();
            if (!result)
                return Result.Fail("Edit profile summary action failed");

            return Result.Ok(request.ProfileSummary);
        }
    }
}
