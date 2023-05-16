using GrpcFreelancerProfile;

namespace Web.Bff.Services
{
    public interface IFreelancerProfileService
    {
        Task<FreelancerBasicData> GetBasicDataByIdAsync(string id);
        Task<FreelancerBasicData> GetBasicDataByUserIdAsync(string userid);
    }
}
