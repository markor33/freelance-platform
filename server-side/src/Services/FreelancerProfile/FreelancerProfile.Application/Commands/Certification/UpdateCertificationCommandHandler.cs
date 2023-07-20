using FluentResults;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class UpdateCertificationCommandHandler : IRequestHandler<UpdateCertificationCommand, Result>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public UpdateCertificationCommandHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<Result> Handle(UpdateCertificationCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(request.FreelancerId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            freelancer.UpdateCertification(request.CertificationId, request.Name,request.Provider, 
                new DateRange(request.Start, request.End), request.Description);


            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync();
            if (!result)
                return Result.Fail("Certification update failed");

            return Result.Ok();
        }
    }
}
