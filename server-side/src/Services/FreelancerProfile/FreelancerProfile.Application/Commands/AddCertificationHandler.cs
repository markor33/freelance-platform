using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entities;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class AddCertificationHandler : IRequestHandler<AddCertificationCommand, Result<Certification>>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public AddCertificationHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<Result<Certification>> Handle(AddCertificationCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByUserIdAsync(request.UserId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            var attended = new DateRange(request.Start, request.End);
            var certification = new Certification(request.Name, request.Provider, attended, request.Description);
            freelancer.AddCertification(certification);

            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!result)
                return Result.Fail("Certification creation failed");
            return Result.Ok(certification);
        }
    }
}
