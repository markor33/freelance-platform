using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using FreelancerProfile.Domain.Repositories;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class UpdateEmploymentCommandHandler : IRequestHandler<UpdateEmploymentCommand, Result<Employment>>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public UpdateEmploymentCommandHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<Result<Employment>> Handle(UpdateEmploymentCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(request.FreelancerId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            freelancer.UpdateEmployment(request.EmploymentId, request.Company, request.Title, 
                new DateRange(request.Start, request.End), request.Description);

            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!result)
                return Result.Fail("Edit employment action failed");

            return Result.Ok();
        }
    }
}
