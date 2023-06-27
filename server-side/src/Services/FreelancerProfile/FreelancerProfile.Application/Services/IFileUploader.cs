using Microsoft.AspNetCore.Http;

namespace FreelancerProfile.Application.Services
{
    public interface IFileUploader
    {
        Task<string> UploadProfilePicture(Guid freelancerId, IFormFile file);
    }
}
