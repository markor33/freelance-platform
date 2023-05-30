using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FreelancerProfile.Application.Commands
{
    public class SetProfilePictureCommand : IRequest<Result<string>>
    {
        public Guid FreelancerId { get; private set; }
        public IFormFile PictureFile { get; private set; }

        public SetProfilePictureCommand(Guid freelancerId, IFormFile pictureFile)
        {
            FreelancerId = freelancerId;
            PictureFile = pictureFile;
        }

    }
}
