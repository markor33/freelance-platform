using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class DeleteEducationEventHandler : IRequestHandler<DeleteEducationCommand, Result>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public DeleteEducationEventHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<Result> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(request.FreelancerId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            freelancer.DeleteEducation(request.EducationId);

            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync();
            if (!result)
                return Result.Fail("Delete education action failed");

            return Result.Ok();
        }
    }
}
