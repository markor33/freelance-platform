using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.Entites;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate.ValueObjects;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class AddCertificationHandler : IRequestHandler<AddCertificationCommand, Certification>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public AddCertificationHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<Certification> Handle(AddCertificationCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByUserIdAsync(request.UserId);
            if (freelancer is null)
                return null;

            var attended = new DateRange(request.Start, request.End);
            var certification = new Certification(request.Name, request.Provider, attended, request.Description);
            freelancer.AddCertification(certification);

            var result = await _freelancerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            if (result == 0)
                return null;
            return certification;
        }
    }
}
