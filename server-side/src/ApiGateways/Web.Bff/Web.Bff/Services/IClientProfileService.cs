using GrpcClientProfile;

namespace Web.Bff.Services
{
    public interface IClientProfileService
    {
        Task<ClientBasicData> GetBasicDataByIdAsync(string id);
        Task<ClientBasicData> GetBasicDataByUserIdAsync(string userId);
    }
}
