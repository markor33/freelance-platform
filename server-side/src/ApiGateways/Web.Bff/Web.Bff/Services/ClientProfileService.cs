using GrpcClientProfile;

namespace Web.Bff.Services
{
    public class ClientProfileService : IClientProfileService
    {
        private readonly ClientProfile.ClientProfileClient _clientProfileClient;

        public ClientProfileService(ClientProfile.ClientProfileClient clientProfileClient)
        {
            _clientProfileClient = clientProfileClient;
        }

        public async Task<ClientBasicData> GetBasicDataByUserIdAsync(string userId)
        {
            return await _clientProfileClient.GetClientBasicDataByUserIdAsync(new GetClientBasicDataByUserIdRequest() { UserId = userId });
        }
    }
}
