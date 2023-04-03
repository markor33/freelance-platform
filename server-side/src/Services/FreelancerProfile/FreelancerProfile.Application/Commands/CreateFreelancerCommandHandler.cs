using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class CreateFreelancerCommandHandler : IRequestHandler<CreateFreelancerCommand, Freelancer>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public CreateFreelancerCommandHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<Freelancer> Handle(CreateFreelancerCommand request, CancellationToken cancellationToken)
        {
            var address = new Address(request.Country, request.City, request.Street, request.Number, request.ZipCode);
            var contact = new Contact(request.TimeZoneId, address, request.PhoneNumber);
            var freelancer = new Freelancer(request.UserId, request.FirstName, request.LastName, contact);

            freelancer = await _freelancerRepository.CreateAsync(freelancer);

            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!result)
                return null;
            return freelancer;
        }

    }
}
