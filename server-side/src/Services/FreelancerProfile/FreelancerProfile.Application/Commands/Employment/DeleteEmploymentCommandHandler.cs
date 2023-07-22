using FluentResults;
using FreelancerProfile.Domain.Repositories;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class DeleteEmploymentCommandHandler : IRequestHandler<DeleteEmploymentCommand, Result>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public DeleteEmploymentCommandHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<Result> Handle(DeleteEmploymentCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(request.FreelancerId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            freelancer.DeleteEmployment(request.EmploymentId);

            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!result)
                return Result.Fail("Delete employment action failed");

            return Result.Ok();
        }
    }
}
