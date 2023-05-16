using GrpcFreelancerProfile;

namespace Web.Bff.Services
{
    public class FreelancerProfileService : IFreelancerProfileService
    {
        private readonly FreelancerProfile.FreelancerProfileClient _freelancerProfileClient;

        public FreelancerProfileService(FreelancerProfile.FreelancerProfileClient freelancerProfileClient)
        {
            _freelancerProfileClient = freelancerProfileClient;
        }

        public async Task<FreelancerBasicData> GetBasicDataByIdAsync(string id)
        {
            return await _freelancerProfileClient.GetFreelancerBasicDataByIdAsync(new GetFreelancerBasicDataByIdRequest() { Id = id });
        }

        public async Task<FreelancerBasicData> GetBasicDataByUserIdAsync(string userId)
        {
            return await _freelancerProfileClient.GetFreelancerBasicDataByUserIdAsync(new GetFreelancerBasicDataByUserIdRequest() { UserId = userId });
        }

    }
}
