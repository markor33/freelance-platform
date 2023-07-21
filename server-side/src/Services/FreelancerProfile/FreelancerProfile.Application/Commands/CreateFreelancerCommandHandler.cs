using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using FreelancerProfile.Domain.Repositories;
using MediatR;


namespace FreelancerProfile.Application.Commands
{
    public class CreateFreelancerCommandHandler : IRequestHandler<CreateFreelancerCommand, bool>
    {
        private readonly IFreelancerRepository _freelancerRepository;

        public CreateFreelancerCommandHandler(IFreelancerRepository freelancerRepository)
        {
            _freelancerRepository = freelancerRepository;
        }

        public async Task<bool> Handle(CreateFreelancerCommand request, CancellationToken cancellationToken)
        {
            var freelancer = Freelancer.Create(request.UserId, request.FirstName, request.LastName, request.Contact);

            await _freelancerRepository.CreateAsync(freelancer);
            var result = await _freelancerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return result;
        }
    }
}
