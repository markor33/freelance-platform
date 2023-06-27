using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FreelancerProfile.Application.Services;
using Microsoft.AspNetCore.Http;

namespace FreelancerProfile.Infrastructure.Services
{
    public class AzureBlobStorageService : IFileUploader
    {
        private readonly BlobContainerClient _containerClient;

        public AzureBlobStorageService()
        {
            _containerClient = new BlobContainerClient(
                "DefaultEndpointsProtocol=https;AccountName=profilepictures123;AccountKey=LN1h7vj+wbFvErkANEIV6/yNYjeRAKmyhAzz9RmVpaIyFQyo+hoeEUxGZMER27SoFFvMW6KfEVBq+AStMzMfAQ==;EndpointSuffix=core.windows.net",
                "profile-pictures");
        }

        public async Task<string> UploadProfilePicture(Guid freelancerId, IFormFile file)
        {
            await DeleteCurrentProfilePicture(freelancerId);

            var fileName = $"{freelancerId}{Path.GetExtension(file.FileName)}";
            using var stream = file.OpenReadStream();
            var response = await _containerClient.UploadBlobAsync(fileName, stream, default);

            var blobClient = _containerClient.GetBlobClient(fileName);
            return blobClient.Uri.ToString();
        }

        private async Task DeleteCurrentProfilePicture(Guid freelancerId)
        {
            var currentProfilePicture = _containerClient.GetBlobsAsync(prefix: freelancerId.ToString());
            await foreach (var picture in currentProfilePicture)
            {
                var existingBlob = _containerClient.GetBlobClient(picture.Name);
                await existingBlob.DeleteAsync();
            }
        }

    }
}
