using GrpcClientProfile;
using GrpcFreelancerProfile;
using Identity.API.Constants;

namespace Identity.API.GrpcServices
{
    public class UserBasicDomainDataService : IUserBasicDomainDataService
    {
        private readonly FreelancerProfile.FreelancerProfileClient _freelancerProfileClient;
        private readonly ClientProfile.ClientProfileClient _clientProfileClient;

        public UserBasicDomainDataService(
            FreelancerProfile.FreelancerProfileClient freelancerProfileClient, 
            ClientProfile.ClientProfileClient clientProfileClient)
        {
            _freelancerProfileClient = freelancerProfileClient;
            _clientProfileClient = clientProfileClient;
        }

        public async Task<UserBasicData> GetBasicDataAsync(Guid userId, string role)
        {
            if (role == Role.CLIENT.ToString())
            {
                var request = new GetClientBasicDataByUserIdRequest() { UserId = userId.ToString() };
                var clientData = await _clientProfileClient.GetClientBasicDataByUserIdAsync(request);
                return new UserBasicData(Guid.Parse(clientData.Id), clientData.FirstName, clientData.LastName);
            }
            else
            {
                var request = new GetFreelancerBasicDataByUserIdRequest() { UserId = userId.ToString() };
                var freelancerData = await _freelancerProfileClient.GetFreelancerBasicDataByUserIdAsync(request);
                return new UserBasicData(Guid.Parse(freelancerData.Id), freelancerData.FirstName, freelancerData.LastName);
            }
        }
    }
}
