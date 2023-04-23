using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class AddEmploymentCommandHandler : IRequestHandler<AddEmploymentCommand, Result<Employment>>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public AddEmploymentCommandHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<Result<Employment>> Handle(AddEmploymentCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByUserIdAsync(request.UserId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            var period = new DateRange(request.Start, request.End);
            var employment = new Employment(request.Company, request.Title, period, request.Description);
            freelancer.AddEmployment(employment);

            var result = await _freelancerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            if (result == 0)
                return Result.Fail("Employment creation failed");
            return Result.Ok(employment);
        }
    }
}
