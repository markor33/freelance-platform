using FluentResults;
using FreelancerProfile.Application.Services;
using FreelancerProfile.Domain.AggregatesModel.FreelancerAggregate;
using MediatR;

namespace FreelancerProfile.Application.Commands
{
    public class SetProfilePictureCommandHandler : IRequestHandler<SetProfilePictureCommand, Result<string>>
    {
        private readonly IFreelancerRepository _freelancerRepository;
        private readonly IFileUploader _fileUploader;

        public SetProfilePictureCommandHandler(IFreelancerRepository freelancerRepository, IFileUploader fileUploader)
        {
            _freelancerRepository = freelancerRepository;
            _fileUploader = fileUploader;
        }

        public async Task<Result<string>> Handle(SetProfilePictureCommand request, CancellationToken cancellationToken)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(request.FreelancerId);
            if (freelancer is null)
                return Result.Fail("Freelancer does not exist");

            var pictureUrl = await _fileUploader.UploadProfilePicture(freelancer.Id, request.PictureFile);
            freelancer.SetProfilePicture(pictureUrl);

            await _freelancerRepository.UnitOfWork.SaveEntitiesAsync();

            return Result.Ok(pictureUrl);
        }
    }
}
