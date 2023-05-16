using GrpcClientProfile;

namespace Web.Bff.Services
{
    public interface IClientProfileService
    {
        Task<ClientBasicData> GetBasicDataByUserIdAsync(string userId);
    }
}
